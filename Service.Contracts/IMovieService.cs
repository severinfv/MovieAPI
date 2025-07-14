using Movies.Shared.DTOs;

namespace Service.Contracts
{
    public interface IMovieService
    {
        Task<bool> MovieExistsAsync(int id);
        Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false);
        //Task<IEnumerable<MovieDto>> GetMoviesAsync(bool includeActors, bool trackChanges = false);
        Task<MovieDetailsDto> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);
    }
}