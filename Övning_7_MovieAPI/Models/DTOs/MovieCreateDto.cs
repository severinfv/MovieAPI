using System.ComponentModel.DataAnnotations;

namespace Övning_7_MovieAPI.Models.DTOs
{
    public record MovieCreateDto([Required][StringLength(100)] string Title, DateOnly Year, int Runtime, double IMDBRating);
}
