using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //remove eventually
using Movies.Infrastructure.Repositories;
using Movies.Shared.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace _Movies.API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        const int maxMoviesPageSize = 10;

        public MoviesController(IUnitOfWork uow)
        {
            this.uow = uow;
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

            var movies = await uow.MovieRepository.GetMoviesAsync();

            var dtos =  movies.Select(m => new MovieDto { Id = m.Id, Title = m.Title, Year = m.Year.Year, Runtime = m.Runtime, IMDBRating = m.IMDBRating }).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            var totalItemCount = dtos.Count();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(dtos);
        }

        // GET: api/Movies/5
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Gets movie by id", Description = "Gets a short info of a movie by Id.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await uow.MovieRepository.GetMovieAsync(id);

            if (movie == null)
                return NotFound();

            var dto = new MovieDto { Id = movie.Id, Title = movie.Title, Year = movie.Year.Year, Runtime = movie.Runtime, IMDBRating = movie.IMDBRating };

            return Ok(dto);
        }


        // GET: api/Movies/5/details
        [HttpGet("{id}/details")]
        [SwaggerOperation(Summary = "Gets movie with details by id", Description = "Gets a detailed info of a movie by Id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieWithDetails(int id)
        {
            var movie = await uow.MovieRepository.GetMovieAsync(id);
            if (movie == null)
                return NotFound();

            var dto = new MovieDetailsDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year.Year,
                DirectorName = movie.Director.Name,
                Runtime = movie.Runtime,
                IMDBRating = movie.IMDBRating,
                Genres = movie.Genres.Select(g => new GenreDto(g.MovieGenre)),
                Actors = movie.Actors.Select(a => new ActorDto(a.Name)),
                Reviews = movie.Reviews.Select(r => new ReviewDto(r.ReviewerName, r.Comment, r.Rating)),
            };



            return Ok(dto);
        }

        
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Movie", Description = "Updates an existing movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await uow.MovieRepository.GetMovieAsync(id, trackChanges: true);

            if (movie is null) return NotFound();

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Runtime = dto.Runtime;
            movie.IMDBRating = dto.IMDBRating;

            await uow.CompleteAsync();
            return NoContent();
        }

        // POST: api/Movies
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

            uow.MovieRepository.Create(movie);
            await uow.CompleteAsync();

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
            var movie = await uow.MovieRepository.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            uow.MovieRepository.Delete(movie);
            await uow.CompleteAsync();

            return NoContent();
        }
    }
}
