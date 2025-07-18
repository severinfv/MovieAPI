namespace Movies.Shared.DTOs.Reports
{
    public class DirectorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int MovieCount { get; set; }
    }
}
