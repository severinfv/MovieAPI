using Domain.Contracts.Repositories;
using Movies.Shared.DTOs;
using Service.Contracts;

namespace Movies.Services;

public class ActorService : IActorService
{
    private IUnitOfWork uow;
    public ActorService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
    public async Task<ActorDto> GetActorAsync(int id, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ActorDto>> GetActorsAsync(int movieId, bool trackChanges = false)
    {
        var movieExists = await uow.MovieRepository.GetMovieAsync(movieId, trackChanges);
        if (movieExists is null) return null!;

        var actors = await uow.ActorRepository.GetActorsAsync(movieId, trackChanges);
        var dtos = actors.Select(a => new ActorDto(a.Name));
        return dtos;
    }
}
