using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(MovieContext context) : base(context) { }

        public async Task<List<Movie>> GetMoviesAsync(bool include = false, bool trackChanges = false)
        {
            return include ? await FindAll(trackChanges).Include(c => c.Actors).ToListAsync() :
                             await FindAll(trackChanges).ToListAsync();
        }
        public async Task<Movie?> GetMovieAsync(int id, bool trackChanges = false)
        {
            return await FindByCondition(m => m.Id == id, trackChanges).FirstOrDefaultAsync();
        }
    }
}
