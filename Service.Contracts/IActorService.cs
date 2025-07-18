using Movies.Shared.DTOs.ActorDTOs;
using Movies.Shared.Parameters;

namespace Service.Contracts
{
    public interface IActorService
    {
        Task<bool> ActorExistsAsync(int id);
        Task<ActorDto> GetActorAsync(int id, bool includeFilms = false, bool trackChanges = false);
        Task<IEnumerable<ActorDto>> GetActorsAsync(ActorParameters parameters, bool trackChanges = false);
        Task<IEnumerable<ActorDto>> GetActorsFromMovieAsync(int movieId, bool trackChanges = false);


    }
}
