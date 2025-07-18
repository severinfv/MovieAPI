using Movies.Shared.DTOs;
using Movies.Shared.Parameters;

namespace Service.Contracts
{
    public interface IMovieService
    {
        Task<bool> MovieExistsAsync(int id);
        Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false);
        Task<MovieDetailsDto> GetMovieWithDetailsAsync(int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);
        Task<PagedList<MovieDto>> GetMoviesAsync(MovieParameters parameters, bool trackChanges = false);
        Task<MovieDto> AddMovieAsync(MovieCreateDto dto, bool trackChanges = false);
        Task UpdateMovieAsync(int id, MovieUpdateDto dto, bool trackChanges = true);
        Task DeleteMovieAsync(int id, bool trackChanges = false);
        void SaveChangesAsync();



    }
}