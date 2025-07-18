namespace Domain.Models.Entities
{
    public class MovieActor
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
        public string? Role { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
        public Actor Actor { get; set; } = null!;

    }
}
