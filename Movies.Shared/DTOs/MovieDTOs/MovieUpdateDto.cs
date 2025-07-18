using System.ComponentModel.DataAnnotations;

namespace Movies.Shared.DTOs.MovieDTOs
{
    public class MovieUpdateDto : MovieManipulationDto
    {
        [Required]
        public int Id { get; init; }
    }
}
