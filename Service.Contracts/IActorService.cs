using Movies.Shared.DTOs;

namespace Service.Contracts
{
    public interface IActorService
    {
        Task<bool> ActorExistsAsync(int id);
        Task<ActorDto> GetActorAsync(int id, bool includeFilms = false, bool trackChanges = false);
        Task<IEnumerable<ActorDto>> GetActorsAsync(bool trackChanges = false);
        Task<IEnumerable<ActorDto>> GetActorsFromMovieAsync(int movieId, bool trackChanges = false);


    }
}
