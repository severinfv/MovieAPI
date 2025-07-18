using Movies.Shared.DTOs;
using Movies.Shared.DTOs.MovieDTOs;
using Movies.Shared.Parameters;

namespace Service.Contracts
{
    public interface IMovieService
    {
        Task<bool> MovieExistsAsync(Guid id);
        Task<MovieDto> GetMovieAsync(Guid id, bool trackChanges = false);
        Task<MovieDetailsDto> GetMovieWithDetailsAsync(Guid id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);
        Task<PagedList<MovieDto>> GetMoviesAsync(MovieParameters parameters, bool trackChanges = false);
        Task<MovieDto> AddMovieAsync(MovieManipulationDto dto, bool trackChanges = false);
        Task UpdateMovieAsync(Guid id, MovieUpdateDto dto, bool trackChanges = true);
        Task DeleteMovieAsync(Guid id, bool trackChanges = false);
        void SaveChangesAsync();



    }
}