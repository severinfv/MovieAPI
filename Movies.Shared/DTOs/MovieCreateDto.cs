using System.ComponentModel.DataAnnotations;

namespace Movies.Shared.DTOs
{
    public record MovieCreateDto([Required][StringLength(100)] string Title, DateOnly Year, int Runtime, double IMDBRating);
}
