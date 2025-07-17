using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Shared.DTOs;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Presentation.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        const int maxMoviesPageSize = 10;

        public MoviesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/movies
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all movies", Description = "Gets a short info of all movies (with pagination)")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies() 
            => Ok((IEnumerable<MovieDto>)await serviceManager.MovieService.GetMoviesAsync());
        //{
        // if (pageSize > maxMoviesPageSize)
        // {
        //     pageSize = maxMoviesPageSize;
        // }

        //.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

        //var totalItemCount = dtos.Count();
        //var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        //Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
       // }

        // GET: api/Movies/5
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Gets movie by id", Description = "Gets a short info of a movie by Id.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> GetMovie(int id) =>
             Ok((MovieDto?) await serviceManager.MovieService.GetMovieAsync(id));
        

        // GET: api/Movies/5/details
        [HttpGet("{id}/details")]
        [SwaggerOperation(Summary = "Gets movie with details by id", Description = "Gets a detailed info of a movie by Id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieWithDetails(int id, bool includeGenres, bool includeActors, bool includeReviews)
            => Ok((MovieDetailsDto?) await serviceManager.MovieService.GetMovieWithDetailsAsync(id, includeGenres, includeActors, includeReviews));

        
        
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Movie", Description = "Updates an existing movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PutMovie(int id, [FromBody] MovieUpdateDto dto)
        {

            await serviceManager.MovieService.UpdateMovieAsync(id, dto, trackChanges: true);

            //if (movie is null) return NotFound();

            return NoContent();
        }
        


        // POST: api/Movies
        [HttpPost]
        [SwaggerOperation(Summary = "Create a movie.", Description = "Creates a new movie.")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieDto>> PostMovie([FromBody] MovieCreateDto dto)
        {
            var movieDto = await serviceManager.MovieService.AddMovieAsync(dto);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);  
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a movie", Description = "Deletes a movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await serviceManager.MovieService.DeleteMovieAsync(id);

            return NoContent();
        } 
  
    }
}
