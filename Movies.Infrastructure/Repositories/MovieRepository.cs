using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> AnyAsync(int id) => await DbSet.AnyAsync(m => m.Id == id);

        public async Task<Movie?> GetMovieAsync(int id, bool trackChanges = false)
        {
            return await FindByCondition(m => m.Id == id, trackChanges).FirstOrDefaultAsync();
        }

        public async Task<Movie?> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false)
        {
            IQueryable<Movie> query = FindByCondition(m => m.Id == id, trackChanges);
            query = query.Include(m => m.Director);
            if (includeGenres) query = query.Include(m => m.Genres);
            if (includeActors) query = query.Include(m => m.Actors);
            if (includeReviews) query = query.Include(m => m.Reviews);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetMoviesAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
        
    }
}
