namespace Domain.Models.Entities
{
    public class MovieDetails : Entity
    {
        public string? Synopsis { get; set; } = null!;
        public double? Revenue { get; set; }
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
