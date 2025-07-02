using System.Net;

namespace Övning_7_MovieAPI.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Year { get; set; } = null!;
        public int Runtime { get; set; }
        public double Rating { get; set; }

        public MovieDetails? MovieDetails { get; set;} = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
