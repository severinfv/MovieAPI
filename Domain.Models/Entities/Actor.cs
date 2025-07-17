namespace Domain.Models.Entities
{
    public class Actor : Entity
    {
        public string Name { get; set; } = null!;
        public DateOnly? Year { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<MovieActor> Roles { get; set; } = new List<MovieActor>();
    }
}
