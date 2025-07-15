using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<bool> AnyAsync(int id);
        Task<Movie?> GetMovieAsync(int id, bool trackChanges = false);
        Task<Movie?> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);
        Task<List<Movie>> GetMoviesAsync(bool trackChanges = false);


    }
}