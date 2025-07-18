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

        public async Task<bool> ExistsAsync(Guid id) => await base.EntityExistsAsync(id);
        public async Task<Movie?> GetByIdAsync(Guid id, bool trackChanges) => await GetEntityByIdAsync(id, trackChanges);
        public async Task<Movie?> GetMovieWithDetailsAsync(Guid id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false)
        {
            IQueryable<Movie> query = FindByCondition(m => m.Id == id, trackChanges);
            query = query.Include(m => m.Director);
            if (includeGenres) query = query.Include(m => m.Genres);
            if (includeActors) query = query.Include(m => m.Actors);
            if (includeReviews) query = query.Include(m => m.Reviews);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PagedList<Movie>> GetAllAsync(MovieParameters parameters, bool trackChanges = false)
        {
            var query = FindAll(trackChanges);

            if (!string.IsNullOrWhiteSpace(parameters.Title))
                query = query.Where(m => m.Title == parameters.Title.Trim());

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery)) //ToDo: Add Searching 
                query = query.Where(m =>
                    m.Title.Contains(parameters.SearchQuery.Trim()));

            return await PagedList<Movie>.PageAsync(query, parameters.PageNumber, parameters.PageSize);
        }

    }
}
