namespace Domain.Models.Entities
{
    public class Movie : Entity
    {
        public string Title { get; set; } = null!;
        public DateOnly Year { get; set; }
        public int Runtime { get; set; }
        public double IMDB { get; set; }
        public double? UsersRating { get; set; }
        public Guid DirectorId { get; set; }
        public Director Director { get; set; } = null!;
        public MovieDetails? MovieDetails { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<MovieActor> Roles { get; set; } = new List<MovieActor>();
    }
}
