using System.ComponentModel.DataAnnotations;

namespace Övning_7_MovieAPI.Models.DTOs
{
    public record MovieCreateDto([Required][StringLength(100)] string Title, string Year, int Runtime, double Rating);
}
