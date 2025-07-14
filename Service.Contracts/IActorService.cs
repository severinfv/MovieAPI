using Movies.Shared.DTOs;

namespace Service.Contracts
{
    public interface IActorService
    {
        Task<ActorDto> GetActorAsync(int id, bool trackChanges = false);
        Task<IEnumerable<ActorDto>> GetActorsAsync(int movieId, bool trackChanges = false);
    }
}
