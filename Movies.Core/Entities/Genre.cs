namespace Movies.Core.Entities
{
    public class Genre : Entity
    {
        public string MovieGenre { get; set; } = null!;
        public ICollection<Movie> Movies { get; } = new List<Movie>();
    }
}
