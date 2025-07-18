using System.ComponentModel.DataAnnotations;

namespace Movies.Shared.DTOs.ReviewDTOs
{
    public record ReviewCreateDto([Required] Guid MovieId, [Required, StringLength(100, MinimumLength = 2)] string ReviewerName, [Required, StringLength(1000, MinimumLength = 3)] string Comment, [Range(0.0, 5.0)] double Rating);
}
