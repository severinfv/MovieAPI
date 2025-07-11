namespace Movies.Shared.DTOs
{
    public class MovieActorCreateDto
    {
        public int ActorId { get; set; }
        public string Role { get; set; } = null!;
    }

}
