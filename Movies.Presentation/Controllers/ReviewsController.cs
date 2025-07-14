using Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using Movies.Shared.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Presentation.Controllers
{

    [ApiController]
    [Route("api/reviews")]
    [Produces("application/json")]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all reviews", Description = "Returns all reviews or filter by reviewer name or rating range.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews(
        [FromQuery] string? reviewerName,
        [FromQuery] string? commentKeyword,
        [FromQuery] double? minRating,
        [FromQuery] double? maxRating)
        {
            double min = minRating ?? 0.0;
            double max = maxRating ?? 5.0;

            if (min > max)
                return BadRequest("Minimum rating cannot be greater than maximum rating.");

            var query = _context.Reviews.AsQueryable();

            if (!string.IsNullOrWhiteSpace(reviewerName))
                query = query.Where(r => r.ReviewerName.ToLower().Contains(reviewerName.ToLower()));

            if (!string.IsNullOrWhiteSpace(commentKeyword))
                query = query.Where(r => r.Comment.ToLower().Contains(commentKeyword.ToLower()));

            query = query.Where(r => r.Rating >= min && r.Rating <= max);

            var result = await query.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a specific review by ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Review>> GetReviewById(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new review", Description = "Creates a new review for an optional movie.")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Review>> CreateReview([FromBody] ReviewCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Provide data json body");

            if (!await _context.Movies.AnyAsync(m => m.Id == dto.MovieId))
                return NotFound($"No movie with ID {dto.MovieId}");

            var review = new Review
            {
                ReviewerName = dto.ReviewerName,
                Comment = dto.Comment,
                Rating = dto.Rating,
                ReviewAdded = DateTime.UtcNow,
                MovieId = dto.MovieId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }
    }
}
