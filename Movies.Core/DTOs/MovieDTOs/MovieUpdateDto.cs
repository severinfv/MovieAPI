using System.ComponentModel.DataAnnotations;

namespace Movies.Core.DTOs.MovieDTOs
{
    public class MovieUpdateDto : MovieManipulationDto
    {
        [Required]
        public Guid Id { get; init; }
    }
}
