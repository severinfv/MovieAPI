using Movies.Shared.DTOs;

namespace Service.Contracts
{
    public interface IMovieService
    {
        Task <MovieDto>GetMovieAsync(int id, bool trackChanges = false);
        Task<IEnumerable<MovieDto>> GetMoviesAsync(bool includeActors, bool trackChanges = false);
    }
}