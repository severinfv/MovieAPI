namespace Övning_7_MovieAPI.Models.DTOs.Reports
{
    public class DirectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int MovieCount { get; set; }
    }
}
