using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Shared.DTOs;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Presentation.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        public readonly IServiceManager serviceManager;
        public ActorsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet("{actorId:int}")]
        [SwaggerOperation(Summary = "Get movie actor", Description = "Gets an actors by Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActorAsync(int actorId, bool includeFilms, bool trackChanges)
        {
            var actorDtos = await serviceManager.ActorService.GetActorAsync(actorId, includeFilms, trackChanges);
            return Ok(actorDtos);

        }

        [HttpGet("movie/{movieId:int}")]
        [SwaggerOperation(Summary = "Get movie actors", Description = "Gets all actors by their MovieId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActorsFromMovieAsync(int movieId, bool trackChanges)
        {
            var actorDtos = await serviceManager.ActorService.GetActorsFromMovieAsync(movieId, trackChanges);
            return Ok(actorDtos);

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all actors", Description = "Gets a list of all actors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActorsAsync()
            => Ok((IEnumerable<ActorDto>)await serviceManager.ActorService.GetActorsAsync());



        /*
        [HttpGet]  // ToDo: trim and nulls
        [SwaggerOperation(Summary = "Get all or filtered Actors", Description = "Gets all actors, or filter by name or search query.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors([FromQuery] string? name, [FromQuery] string? searchquery)
        {
            var collection = _context.Actors as IQueryable<Actor>;
            if (!string.IsNullOrWhiteSpace(name))
                collection = collection?.Where(a => a.Name == name);

            if (!string.IsNullOrWhiteSpace(searchquery))
                collection = collection?.Where(a => a.Name.Contains(searchquery));

            var act = await collection.ToListAsync();

            var res = act.Select(a => new ActorDto(a.Name));
            return Ok(res);
        }


        // POST: api/movies/")
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [SwaggerOperation(Summary = "Add an actor to a movie", Description = "Add an actor to a movie, with or without a role.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("{movieId}/actors/")]
        public async Task<ActionResult> PostActorMovie(int movieId, int? actorId, bool? includeRole = false, [FromBody] MovieActorCreateDto? addRole = null)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
                return NotFound($"No movie with id {movieId} in database");

            int ActorId;
            string? role = null;

            if (includeRole == true)
            {
                if (addRole == null)
                    return BadRequest("Provide actorId and role in json body");

                ActorId = addRole.ActorId;
                role = addRole.Role;
            }
            else
            {
                if (actorId == null)
                    return BadRequest("Provide actorId in query when includeRole is false");

                ActorId = actorId.Value;
            }

            var actor = await _context.Actors.FindAsync(ActorId);
            if (actor == null)
                return NotFound($"No actor with id {ActorId} in database");

            bool alreadyAssigned = await _context.Roles
                .AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == ActorId);

            if (alreadyAssigned)
                return Conflict($"Actor {actor.Name} is already assigned to movie {movie.Title}");

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = ActorId,
                Role = role
            };

            _context.Roles.Add(movieActor);
            await _context.SaveChangesAsync();

            var message = includeRole == true
                    ? $"Actor {actor.Name} added to movie {movie.Title} with role '{role}'"
                    : $"Actor {actor.Name} added to movie {movie.Title}";

            return Ok(message);


        }
        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
        */
    }
}
