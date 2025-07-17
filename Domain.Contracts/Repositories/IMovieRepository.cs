using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<bool> ExistsAsync(int id);
        Task<List<Movie>> GetAllAsync(bool trackChanges = false);
        Task<Movie?> GetByIdAsync(int id, bool trackChanges);
        Task<Movie?> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);


    }
}