using Bogus;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Context;

namespace Movies.Infrastructure.Data;
public class UsersDataSeed
{
    public static async Task InitAsync(ApplicationDbContext context, int userCount = 10, int minReviews = 3, int maxReviews = 10, int  movieLimit=15)
    {
        Console.WriteLine("Seed Users + Reviews: Starting...");
        if (await context.Users.AnyAsync())
        {
            Console.WriteLine("Seed Users + Reviews: Skip (already exists).");
            return;
        }
        var movieIds = await context.Movies
            .Take(movieLimit)
            .Select(m => m.Id)
            .ToListAsync();

        if (!movieIds.Any())
        {
            Console.WriteLine("No movies found.");
            return;
        }

        var faker = new Faker("en");
        var users = new List<ApplicationUser>();

        for (int i = 0; i < userCount; i++)
        {
            var user = new ApplicationUser
            {
                UserName = faker.Internet.UserName(),
                DateRegistered = faker.Date.RecentDateOnly(10),
                Reviews = new List<Review>()
            };

            int reviewCount = faker.Random.Int(minReviews, Math.Min(maxReviews, movieIds.Count));
            var reviewedMovies = faker.PickRandom(movieIds, reviewCount).ToList();

            foreach (var movieId in reviewedMovies)
            {
                var review = new Review
                {
                    MovieId = movieId,
                    UserRating = faker.Random.Int(1, 10),
                    UserComment = faker.Lorem.Sentence(),
                    DateAdded = DateOnly.FromDateTime(DateTime.Now)
                };

                user.Reviews.Add(review);
            }

            users.Add(user);
        }
            await context.AddRangeAsync(users);
            await context.SaveChangesAsync();
            Console.WriteLine($"SeedData: {movieIds.Count()} movies to insert.");
        
    }
}
