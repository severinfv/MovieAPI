using CsvHelper;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data.raw;
using System.Globalization;

namespace Movies.Infrastructure.Data
{
    public class SeedData
    {
        internal static async Task InitAsync(ApplicationDbContext context)
        {
            Console.WriteLine("SeedData: Starting...");
            if (await context.Movies.AnyAsync())
            {
                //await context.Database.EnsureDeletedAsync();
                //await context.Database.EnsureCreatedAsync();
                //await InitAsync(context);
                Console.WriteLine("SeedData: Skip.");
                return;
            }

            var movies = GenerateMovies();
            await context.AddRangeAsync(movies);
            Console.WriteLine($"SeedData: {movies.Count()} movies to insert.");
            await context.SaveChangesAsync();
            Console.WriteLine($"SeedData: Done.");
        }

        public static IEnumerable<Movie> GenerateMovies()
        {
            var movies = new List<Movie>();

            //to avoid duplicates
            var genreCheck = new Dictionary<string, Genre>(StringComparer.OrdinalIgnoreCase);
            var actorCheck = new Dictionary<string, Actor>(StringComparer.OrdinalIgnoreCase);
            var directorCheck = new Dictionary<string, Director>(StringComparer.OrdinalIgnoreCase);

            using var reader = new StreamReader("Data/raw/raw.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CsvRecord>();

            foreach (var record in records)
            {
                var directorName = record.Director.Trim();
                if (!directorCheck.TryGetValue(directorName, out var director))
                {
                    director = new Director { Name = directorName };
                    directorCheck[directorName] = director;
                }

                double? revenue = null;
                if (double.TryParse(record.Revenue, out var parsedRevenue))
                    revenue = parsedRevenue;

                var movie = new Movie()
                {
                    Title = record.Title.Trim(),
                    Year = new DateOnly(int.Parse(record.Year), 12, 31),
                    Runtime = record.Runtime,
                    MovieDetails = new MovieDetails()
                    {
                        Synopsis = record.Description.Trim(),
                        Revenue = revenue
                    },
                    IMDB = record.Rating,
                    Director = director,
                };


                foreach (var mg in record.Genre.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!genreCheck.TryGetValue(mg.Trim(), out var genre))
                    {
                        genre = new Genre { MovieGenre = mg.Trim() };
                        genreCheck[mg.Trim()] = genre;
                    }
                    movie.Genres.Add(genre);
                }

                foreach (var a in record.Actors.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {

                    if (!actorCheck.TryGetValue(a.Trim(), out var actor))
                    {
                        actor = new Actor { Name = a.Trim() };
                        actorCheck[a.Trim()] = actor;
                    }
                    movie.Actors.Add(actor);
                }
                movies.Add(movie);
            }
            return movies;
        }
    }
}
