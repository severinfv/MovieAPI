namespace Övning_7_MovieAPI.Models.DTOs
{
    public record MovieUpdateDto(string Title, DateOnly Year, int Runtime, double IMDBRating);
}
