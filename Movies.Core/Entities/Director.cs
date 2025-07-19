using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Core.Entities
{
    [Table("Director")]
    public class Director : Entity
    {
        public string Name { get; set; } = null!;
        public string? Biography { get; set; } = null!;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
