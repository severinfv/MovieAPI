using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Övning_7_MovieAPI.Data;
using Övning_7_MovieAPI.Models.DTOs;
using Övning_7_MovieAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Övning_7_MovieAPI.Controllers
{ 
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ActorsController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]  //trim and nulls
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActor([FromQuery] string? name, [FromQuery] string? searchquery)
        {
            var collection = _context.Actor as IQueryable<Actor>;
            // var actors = await _context.Actor.Where(a => a.Name == name).Select(a => new ActorDto(a.Name, a.BirthYear)).ToListAsync();
            if (!string.IsNullOrWhiteSpace(name))
                collection = collection?.Where(a => a.Name == name);

            if (!string.IsNullOrWhiteSpace(searchquery))
                collection = collection?.Where(a => a.Name.Contains(searchquery));

            var act = await collection.ToListAsync();

            var res = act.Select(a => new ActorDto(a.Name, a.BirthYear));
            return Ok(res);
        }



        // POST: api/movies/{movieId}/actors")
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{movieId}/actors/{actorId}")]
        public async Task<ActionResult> PostActorMovie(int movieId, int actorId)
        {
            var movie = await _context.Movies.Include(m => m.Actors).Where(m => m.Id == movieId).FirstOrDefaultAsync();
            if (movie == null)
                return NotFound($"No movie with id {movieId}");

            var actor = await _context.Actor.Where(a => a.Id == actorId).FirstOrDefaultAsync();
            if (actor == null)
                return NotFound($"No actor with id {actorId}");
            
            movie.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return Ok($"Actor {actor.Name} added to a movie {movie.Title}");
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return _context.Actor.Any(e => e.Id == id);
        }


    }
}
