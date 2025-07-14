using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<List<Movie>> GetMoviesAsync(bool include = false, bool trackChanges = false);
        Task<Movie?> GetMovieAsync(int id, bool trackChanges = false);
    }
}