namespace Movies.Shared.DTOs
{
    public class MovieActorCreateDto
    {
        public Guid ActorId { get; set; }
        public string Role { get; set; } = null!;
    }

}
