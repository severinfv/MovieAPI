using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Contracts;
using Movies.Core.DTOs.MovieDTOs;
using Movies.Core.DTOs.ReportsDTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Presentation.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [Produces("application/json")]
    public class ReportsController : ControllerBase
    {
        public readonly IServiceManager serviceManager;
        public ReportsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        /*
        // GET: api/reports/genres/popular5
        [HttpGet("genres/popular{nr:int}")]
        [SwaggerOperation(Summary = "Get top genres by movie count", Description = "Returns the most frequently used genres in movies.", Tags = ["Reports on Genres"])]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TopGenreDto>))]
        public async Task<ActionResult<List<TopGenreDto>>> PopularGenres(int nr)
        {
            var popularGenres = await _context.Genres
                .Select(mg => new TopGenreDto { Genre = mg.MovieGenre, Movies = mg.Movies.Count })
                .OrderByDescending(g => g.Movies).Take(nr).ToListAsync();

            return Ok(popularGenres);
        }


        // GET: api/reports/genres/imdbtop5
        [HttpGet("genres/imdbtop{nr:int}")]
        [SwaggerOperation(Summary = "Get top genres by IMDb rating", Description = "Returns genres ranked by the average IMDb rating of their movies.", Tags = ["Reports on Genres"])]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GenreAvgScore>))]
        public async Task<ActionResult<IEnumerable<GenreAvgScore>>> GetTopGenres(int nr)
        {
            var topgenres = await _context.Movies
                .SelectMany(m => m.Genres, (m, mg) => new { mg.MovieGenre, m.IMDB })
                .GroupBy(g => g.MovieGenre)
                .Select(ms => new GenreAvgScore { Genre = ms.Key, AvgRating = Math.Round(ms.Average(x => x.IMDB), 2) })
                .OrderByDescending(g => g.AvgRating).Take(nr).ToListAsync();

            return Ok(topgenres);
        }

        // GET: api/reports/actors/prolific5 
        [HttpGet("actors/prolific{nr:int}")]
        [SwaggerOperation(Summary = "Get top prolific actors", Description = "Returns actors who appear in the most movies.", Tags = ["Reports on Actors"])]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TopGenreDto>))]
        public async Task<ActionResult<IEnumerable<TopGenreDto>>> GetProlificActors(int nr)
        {
            var prolific = await _context.Actors
                .Select(mg => new TopGenreDto { Genre = mg.Name, Movies = mg.Movies.Count })
                .OrderByDescending(g => g.Movies).Take(nr).ToListAsync();

            return Ok(prolific);
        }


        // GET: api/reports/actors/versatile5
        [SwaggerOperation(Summary = "Get versatile actors", Description = "Returns actors who have acted across the most different genres.", Tags = ["Reports on Actors"])]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TopGenreDto>))]
        [HttpGet("actors/versatile{nr:int}")]
        public async Task<ActionResult<IEnumerable<TopGenreDto>>> GetVersatileActors(int nr)
        {
            var versatile = await _context.Actors
                .Select(a => new
                {
                    a.Name,
                    GenresPlayed = a.Movies.SelectMany(m => m.Genres.Select(g => g.MovieGenre)).Distinct().Count()
                })
                .OrderByDescending(g => g.GenresPlayed).Take(nr).ToListAsync();

            return Ok(versatile);
        }

        // GET: api/reports/collabs 
        [HttpGet("collabs")]
        [SwaggerOperation(Summary = "Get top actor-director collaborations", Description = "Returns actor-director pairs with the most shared movies.", Tags = ["Reports on Directors", "Reports on Actors"])]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CollaborationsDto>))]
        public async Task<ActionResult<IEnumerable<CollaborationsDto>>> GetCollaborations()
        {

            var collabs = await _context.Movies
                .SelectMany(m => m.Actors, (m, a) => new { m, actor = a.Name, director = m.Director.Name })
                .GroupBy(g => new { g.actor, g.director })
                .Select(ad => new CollaborationsDto
                {
                    Director = ad.Key.director,
                    Actor = ad.Key.actor,
                    Count = ad.Count(),
                    Movies = ad.Select(g => new MovieDto
                    {
                        Id = g.m.Id,
                        Title = g.m.Title,
                        Year = g.m.Year.Year,
                        Runtime = g.m.Runtime,
                        IMDBRating = g.m.IMDB
                    })
                    .ToList()
                })
                .OrderByDescending(g => g.Count).Take(40).ToListAsync();

            return Ok(collabs);
        }

        [HttpGet("directors")]
        [SwaggerOperation(Summary = "Get recent directors", Description = "Returns directors who have made at least one movie since 2015.", Tags = ["Reports on Directors"])]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DirectorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DirectorDto>>> GetRecentDirectors([FromQuery] int year = 2000)
        {
            var currentYear = DateTime.Now.Year;

            if (year > currentYear)
            {
                return BadRequest($"Invalid year {year}. Year must not be in the future (max: {currentYear}).");
            }

            var recentDirectors = await _context.Directors
                .Where(d => d.Movies.Any(m => m.Year.Year >= year))
                .Select(d => new DirectorDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    MovieCount = d.Movies.Count(m => m.Year.Year >= year)
                })
                .OrderByDescending(d => d.MovieCount)
                .ToListAsync();

            if (recentDirectors.Count == 0)
            {
                return Ok(new { message = $"No directors found with movies after {year}.", data = recentDirectors });
            }

            return Ok(recentDirectors);

        } */

    }
}
