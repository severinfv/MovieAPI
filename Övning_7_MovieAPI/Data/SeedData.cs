using CsvHelper;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Övning_7_MovieAPI.Models.Entities;
using Övning_7_MovieAPI.Data.raw;

namespace Övning_7_MovieAPI.Data
{
    public class SeedData
    {
        internal static async Task InitAsync(MovieContext context)
        {
            Console.WriteLine("SeedData: Starting...");
            if (await context.Movies.AnyAsync())
            {
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

            using var reader = new StreamReader("Data/raw/raw.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CsvRecord>();

            foreach (var record in records)
            {
                //string[] actors = record.Actors.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);

                var movie = new Movie()
                {
                    Title = record.Title.Trim(),
                    Year = record.Year,
                    Runtime = record.Runtime,
                    MovieDetails = new MovieDetails()
                    {
                        Synopsis = record.Description.Trim(),
                        Language = "English"
                    },
                    Rating = record.Rating,
                };

                foreach (var mg in record.Genre.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    var genre = new Genre
                    {
                        MovieGenre = mg.Trim(),
                    };
                    movie.Genres.Add(genre);
                }

                foreach (var a in record.Actors.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    var actor = new Actor
                    {
                        Name = a.Trim(),
                        BirthYear = new DateTime(1970, 1, 1) // placeholder
                    };
                    movie.Actors.Add(actor);
                }
                movies.Add(movie);
            }
            return movies;
        }
    }
}
