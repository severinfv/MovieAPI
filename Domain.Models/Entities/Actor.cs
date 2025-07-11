namespace Domain.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly? Year { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<MovieActor> Roles { get; set; } = new List<MovieActor>();
    }
}
