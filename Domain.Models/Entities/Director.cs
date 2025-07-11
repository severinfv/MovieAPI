using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("Director")]
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
