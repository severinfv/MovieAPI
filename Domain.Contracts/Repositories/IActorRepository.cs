using Domain.Models.Entities;
using Movies.Shared.Parameters;

namespace Domain.Contracts.Repositories
{
    public interface IActorRepository : IRepositoryBase<Actor>
    {
        Task<bool> ExistsAsync(int id);
        Task<PagedList<Actor>> GetAllAsync(EntityParameters parameters, bool trackChanges = false);
        Task<Actor?> GetByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Actor>> GetActorsByMovieIdAsync(int movieId, bool trackChanges);

    }
}