using System.Net;

namespace Övning_7_MovieAPI.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Year { get; set; } 
        public int Duration { get; set; }

        public MovieDetails? MovieDetails { get; set;} = null!;
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<Actor> Actors { get; } = new List<Actor>();
        public ICollection<Genre> Genres { get; } = new List<Genre>();
    }
}
