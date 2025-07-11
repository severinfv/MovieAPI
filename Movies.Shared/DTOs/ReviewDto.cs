using System.ComponentModel.DataAnnotations;

namespace Movies.Shared.DTOs
{
    public record ReviewDto(string ReviewerName, string Comment, double Rating);
}
