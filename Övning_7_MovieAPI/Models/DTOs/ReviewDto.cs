using System.ComponentModel.DataAnnotations;

namespace Övning_7_MovieAPI.Models.DTOs
{
    public record ReviewDto(string ReviewerName, string Comment, double Rating);
}
