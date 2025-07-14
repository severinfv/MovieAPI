namespace Domain.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime ReviewAdded { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public double Rating { get; set; }
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }

    }
}
