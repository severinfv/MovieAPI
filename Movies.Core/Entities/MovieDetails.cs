namespace Movies.Core.Entities
{
    public class MovieDetails : Entity
    {
        public string? Synopsis { get; set; } = null!;
        public double? Budget { get; set; }
        public double? Revenue { get; set; }
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
