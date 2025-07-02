using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Övning_7_MovieAPI.Data;
using Övning_7_MovieAPI.Models.Entities;
using Övning_7_MovieAPI.Models.DTOs;
using System.Text.Json;

namespace Övning_7_MovieAPI.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;
        const int maxMoviesPageSize = 10;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<(IEnumerable<MovieDto>, PaginationMetadata)>> GetMovie(int pageNumber = 1, int pageSize = 5)
        {
            if (pageSize > maxMoviesPageSize)
            {
                pageSize = maxMoviesPageSize;
            }
            var movies = await _context.Movies.Select(m => new MovieDto(m.Id, m.Title,  m.Year, m.Runtime, m.Rating)).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
            
            var totalItemCount = await _context.Movies.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _context.Movies.Where(m => m.Id == id).Select(m => new MovieDto(m.Id, m.Title, m.Year, m.Runtime, m.Rating)).FirstOrDefaultAsync();
            

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // GET: api/Movies/5
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieWithDetails(int id)
        {
            var movie = await _context.Movies.Where(m => m.Id == id).Select(m => new MovieDetailsDto 
            { 
                Id = m.Id, 
                Title = m.Title, 
                Year = m.Year, 
                Runtime = m.Runtime, 
                Rating = m.Rating,
                Genres = m.Genres.Select(g => new GenreDto(g.MovieGenre)),
                Actors = m.Actors.Select(a => new ActorDto(a.Name, a.BirthYear)),
                Reviews = m.Reviews.Select(r => new ReviewDto(r.ReviewerName, r.Comment, r.Rating)),
            })
            .FirstOrDefaultAsync();


            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(s => s.Id == id);

            if (movie is null) return NotFound();

            movie.Title = dto.Title;
            movie.Year = dto.Year;  
            movie.Runtime = dto.Runtime;    
            movie.Rating = dto.Rating;

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
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Runtime = dto.Runtime,
                Rating = dto.Rating,
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto(movie.Id, movie.Title, movie.Year, movie.Runtime, movie.Rating);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
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
