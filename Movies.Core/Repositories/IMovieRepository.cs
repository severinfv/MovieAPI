using Movies.Core.Entities;
using Movies.Core.Parameters;

namespace Movies.Core.Repositories
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<bool> ExistsAsync(Guid id);
        Task<PagedList<Movie>> GetAllAsync(MovieParameters parameters, bool trackChanges = false);
        Task<Movie?> GetByIdAsync(Guid id, bool trackChanges);
        Task<Movie?> GetMovieWithDetailsAsync(Guid id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false);


    }
}