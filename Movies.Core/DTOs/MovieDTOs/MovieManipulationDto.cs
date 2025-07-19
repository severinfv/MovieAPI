using System.ComponentModel.DataAnnotations;

namespace Movies.Core.DTOs.MovieDTOs
{
    public class MovieManipulationDto
    {
        [Required(ErrorMessage = "Title is required")][StringLength(100)] public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Year is required in a yyyy-mm-dd format")] public DateOnly Year { get; set; }
        public int Runtime { get; set; }
        public double IMDB { get; set; }
        [Required(ErrorMessage = "DirectorId is required")] public Guid DirectorId { get; set; }
    }
}
