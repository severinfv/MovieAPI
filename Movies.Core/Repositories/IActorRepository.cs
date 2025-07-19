using Movies.Core.Entities;
using Movies.Core.Parameters;

namespace Movies.Core.Repositories
{
    public interface IActorRepository : IRepositoryBase<Actor>
    {
        Task<bool> ExistsAsync(Guid id);
        Task<PagedList<Actor>> GetAllAsync(EntityParameters parameters, bool trackChanges = false);
        Task<Actor?> GetByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Actor>> GetActorsByMovieIdAsync(Guid movieId, bool trackChanges);

    }
}