using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Shared.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace Movies.API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;
        const int maxMoviesPageSize = 10;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/movies
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all movies", Description = "Gets a short info of all movies (with pagination)")]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<(IEnumerable<MovieDto>, PaginationMetadata)>> GetMovie(int pageNumber = 1, int pageSize = 5)
        {
            if (pageSize > maxMoviesPageSize)
            {
                pageSize = maxMoviesPageSize;
            }
            var movies = await _context.Movies.Select(m => new MovieDto { Id = m.Id, Title = m.Title, Year = m.Year.Year, Runtime = m.Runtime, IMDBRating = m.IMDBRating }).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            var totalItemCount = await _context.Movies.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Gets movie by id", Description = "Gets a short info of a movie by Id.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _context.Movies.Where(m => m.Id == id).Select(m => new MovieDto { Id = m.Id, Title = m.Title, Year = m.Year.Year, Runtime = m.Runtime, IMDBRating = m.IMDBRating }).FirstOrDefaultAsync();


            if (movie == null)
                return NotFound();

            return Ok(movie);
        }


        // GET: api/Movies/5/details
        [HttpGet("{id}/details")]
        [SwaggerOperation(Summary = "Gets movie with details by id", Description = "Gets a detailed info of a movie by Id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieWithDetails(int id)
        {
            var movie = await _context.Movies.Where(m => m.Id == id).Select(m => new MovieDetailsDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year.Year,
                DirectorName = m.Director.Name,
                Runtime = m.Runtime,
                IMDBRating = m.IMDBRating,
                Genres = m.Genres.Select(g => new GenreDto(g.MovieGenre)),
                Actors = m.Actors.Select(a => new ActorDto(a.Name)),
                Reviews = m.Reviews.Select(r => new ReviewDto(r.ReviewerName, r.Comment, r.Rating)),
            })
            .FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Movie", Description = "Updates an existing movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(s => s.Id == id);

            if (movie is null) return NotFound();

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Runtime = dto.Runtime;
            movie.IMDBRating = dto.IMDBRating;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(Summary = "Create a movie.", Description = "Creates a new movie.")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Runtime = dto.Runtime,
                IMDBRating = dto.IMDBRating,
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto { Id = movie.Id, Title = movie.Title, Year = movie.Year.Year, Runtime = movie.Runtime, IMDBRating = movie.IMDBRating };
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a movie", Description = "Deletes a movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
