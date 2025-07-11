using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext context;
        public MovieRepository(MovieContext context)
        {
            this.context = context;
        }
        public async Task<List<Movie>> GetMoviesAsync(bool include = false)
        {
            return include ? await context.Movies.Include(c => c.Actors).ToListAsync() :
                   await context.Movies.ToListAsync();
        }
        public async Task<Movie?> GetMovieAsync(int id)
        {
            return await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }
        public void Create(Movie movie) => context.Movies.Add(movie);

        public void Update(Movie movie) => context.Movies.Update(movie);

        public void Delete(Movie movie) => context.Movies.Remove(movie);
    }
}
