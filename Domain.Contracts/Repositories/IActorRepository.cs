using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetActorsAsync(int movieId, bool trackChanges = false);
    }
}