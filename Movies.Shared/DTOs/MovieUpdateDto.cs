namespace Movies.Shared.DTOs
{
    public record MovieUpdateDto(string Title, DateOnly Year, int Runtime, double IMDBRating);
}
