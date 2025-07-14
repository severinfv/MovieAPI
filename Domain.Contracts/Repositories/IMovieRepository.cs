using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<bool> AnyAsync(int id);
        Task<List<Movie>> GetMoviesAsync(bool include = false, bool trackChanges = false);
        Task<Movie?> GetMovieAsync(int id, bool trackChanges = false);
        Task<Movie?> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);

    }
}