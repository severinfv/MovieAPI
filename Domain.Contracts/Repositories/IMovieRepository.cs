using Domain.Models.Entities;
using Movies.Shared.Parameters;

namespace Domain.Contracts.Repositories
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<bool> ExistsAsync(int id);
        Task<PagedList<Movie>> GetAllAsync(MovieParameters parameters, bool trackChanges = false);
        Task<Movie?> GetByIdAsync(int id, bool trackChanges);
        Task<Movie?> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);


    }
}