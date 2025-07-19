namespace Movies.Core.DTOs
{
    public class MovieActorCreateDto
    {
        public Guid ActorId { get; set; }
        public string Role { get; set; } = null!;
    }

}
