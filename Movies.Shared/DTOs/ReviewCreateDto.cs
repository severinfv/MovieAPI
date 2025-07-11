using System.ComponentModel.DataAnnotations;

namespace Movies.Shared.DTOs
{
    public record ReviewCreateDto([Required] int MovieId, [Required, StringLength(100, MinimumLength = 2)] string ReviewerName, [Required, StringLength(1000, MinimumLength = 3)] string Comment, [Range(0.0, 5.0)] double Rating);
}
