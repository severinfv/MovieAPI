namespace Övning_7_MovieAPI.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthYear { get; set; }
        public ICollection<Movie> Movie { get; } = new List<Movie>();
    }
}
