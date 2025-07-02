namespace Övning_7_MovieAPI.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string BirthYear { get; set; } = null!;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
