using Movies.Core.DTOs;
using Movies.Core.DTOs.MovieDTOs;
using Movies.Core.Parameters;

namespace Movies.Contracts
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