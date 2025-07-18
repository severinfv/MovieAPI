using System.ComponentModel.DataAnnotations;

namespace Movies.Shared.DTOs
{
    public class MovieCreateDto
    {
        [Required(ErrorMessage = "Title is required")][StringLength(100)] public string Title { get; set; }

        [Required(ErrorMessage = "Year is required in a yyyy-mm-dd format")] public DateOnly Year { get; set; }
        public int Runtime { get; set; }
        public double IMDBRating { get; set; }
        [Required(ErrorMessage = "DirectorId is required")] public int DirectorId { get; set; }
    }
}
