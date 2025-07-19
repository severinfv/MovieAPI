namespace Movies.Core.DTOs.ReportsDTOs
{
    public class DirectorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int MovieCount { get; set; }
    }
}
