namespace Domain.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string MovieGenre { get; set; } = null!;
        public ICollection<Movie> Movies { get; } = new List<Movie>();
    }
}
