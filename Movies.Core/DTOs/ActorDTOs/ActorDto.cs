using Movies.Core.DTOs.MovieDTOs;

namespace Movies.Core.DTOs.ActorDTOs
{
    public class ActorDto
    {
        public required string Name { get; set; }
        public IEnumerable<MovieDto> Appeared { get; set; } = Enumerable.Empty<MovieDto>();

    }
}
