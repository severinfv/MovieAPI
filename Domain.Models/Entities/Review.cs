namespace Domain.Models.Entities
{
    public class Review : Entity
    {
        public DateTime ReviewAdded { get; set; }
        public string UserName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public double Rating { get; set; }
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }

    }
}
