using Movies.Shared.DTOs.MovieDTOs;

namespace Movies.Shared.DTOs.ActorDTOs
{
    public class ActorDto
    {
        public required string Name { get; set; }
        public IEnumerable<MovieDto> Appeared { get; set; } = Enumerable.Empty<MovieDto>();

    }
}
