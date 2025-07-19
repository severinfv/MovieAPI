namespace Movies.Core.Entities
{
    public class Review : Entity
    {
        public DateOnly DateAdded { get; set; }
        public string? UserComment { get; set; } = null!;
        public double? UserRating { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

    }
}
