using Movies.Core.DTOs.ActorDTOs;
using Movies.Core.DTOs.MovieDTOs;
using Movies.Core.Exceptions;
using Movies.Core.Parameters;
using Movies.Core.Repositories;
using Movies.Contracts;

namespace Movies.Services;

public class ActorService : IActorService
{
    private IUnitOfWork uow;
    public ActorService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
    public async Task<bool> ActorExistsAsync(Guid id) => await uow.ActorRepository.ExistsAsync(id);

    public async Task<ActorDto> GetActorAsync(Guid id, bool includeFilms = false, bool trackChanges = false)
    {
        var actor = await uow.ActorRepository.GetByIdAsync(id, trackChanges) ?? throw new ActorNotFoundException(id);

        var dto = new ActorDto
        {
            Name = actor.Name,
            Appeared = includeFilms
                    ? actor.Movies.Select(g => new MovieDto { Title = g.Title, Year = g.Year.Year })
                    : Enumerable.Empty<MovieDto>(),
        };
        return dto;
    }

    public async Task<IEnumerable<ActorDto>> GetActorsAsync(ActorParameters parameters, bool trackChanges = false)
    {
        var actors = await uow.ActorRepository.GetAllAsync(parameters, trackChanges);

        var dtos = actors.Select(a => new ActorDto { Name = a.Name });
        return dtos;
    }

    public async Task<IEnumerable<ActorDto>> GetActorsFromMovieAsync(Guid movieId, bool trackChanges = false)
    {
        if (!await uow.MovieRepository.ExistsAsync(movieId))
            new MovieNotFoundException(movieId);

        var actors = await uow.ActorRepository.GetActorsByMovieIdAsync(movieId, trackChanges);

        var dtos = actors.Select(a => new ActorDto { Name = a.Name });

        return dtos;
    }



}
