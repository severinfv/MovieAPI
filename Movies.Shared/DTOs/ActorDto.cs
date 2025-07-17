namespace Movies.Shared.DTOs
{
    public class ActorDto
    {
        public required string Name { get; set; }
        public IEnumerable<MovieDto> Appeared { get; set; } = Enumerable.Empty<MovieDto>();

    }
}
