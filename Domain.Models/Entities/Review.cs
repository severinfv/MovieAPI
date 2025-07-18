namespace Domain.Models.Entities
{
    public class Review : Entity
    {
        public DateTime ReviewAdded { get; set; }
        public string? ReviewText { get; set; } = null!;
        public double UserRating { get; set; }
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
