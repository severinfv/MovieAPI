using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IActorRepository : IRepositoryBase<Actor>
    {
        Task<bool> ExistsAsync(int id);
        Task<List<Actor>> GetAllAsync(bool trackChanges = false);
        IQueryable<Actor> GetAllForSearchAsync(bool trackChanges = false);
        Task<Actor?> GetByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Actor>> GetActorsByMovieIdAsync(int movieId, bool trackChanges);

    }
}