using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using Movies.Shared.DTOs;
using Movies.Shared.Parameters;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id) => await base.EntityExistsAsync(id);
        public async Task<Movie?> GetByIdAsync(int id, bool trackChanges) => await GetEntityByIdAsync(id, trackChanges);
       // public async Task<PagedList<Movie>> GetAllAsync(MovieParameters parameters, bool trackChanges = false)
        //    => await PagedList<Movie>.PageAsync(FindAll(trackChanges), parameters.PageNumber, parameters.PageSize);

        public async Task<Movie?> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false)
        {
            IQueryable<Movie> query = FindByCondition(m => m.Id == id, trackChanges);
            query = query.Include(m => m.Director);
            if (includeGenres) query = query.Include(m => m.Genres);
            if (includeActors) query = query.Include(m => m.Actors);
            if (includeReviews) query = query.Include(m => m.Reviews);

            return await query.FirstOrDefaultAsync();
        }
        
    }
}
