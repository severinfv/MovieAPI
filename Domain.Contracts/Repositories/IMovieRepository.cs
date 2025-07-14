using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetMovieAsync(int id, bool trackChanges = false);
        Task<List<Movie>> GetMoviesAsync(bool include = false, bool trackChanges = false);
        void Create(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
    }
}