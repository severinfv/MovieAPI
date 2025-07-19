using Movies.Core.DTOs.ActorDTOs;
using Movies.Core.DTOs.ReviewDTOs;

namespace Movies.Core.DTOs
{
    public class MovieDetailsDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required int Year { get; set; }
        public required string DirectorName { get; set; }
        public required int Runtime { get; set; }
        public required double IMDBRating { get; set; }
        // public string? Language { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; } = Enumerable.Empty<GenreDto>();
        public IEnumerable<ActorDto> Actors { get; set; } = Enumerable.Empty<ActorDto>();
        public IEnumerable<ReviewDto> Reviews { get; set; } = Enumerable.Empty<ReviewDto>();

    }
}
