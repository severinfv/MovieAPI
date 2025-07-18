namespace Movies.Shared.DTOs.MovieDTOs
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public int Runtime { get; set; }
        public double IMDBRating { get; set; }
    }
}
