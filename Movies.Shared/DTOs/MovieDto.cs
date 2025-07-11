namespace Movies.Shared.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public int Runtime { get; set; }
        public double IMDBRating { get; set; }
    }
}
