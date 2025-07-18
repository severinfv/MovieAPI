﻿using Movies.Core.DTOs.ActorDTOs;
using Movies.Core.Parameters;

namespace Service.Contracts
{
    public interface IActorService
    {
        Task<bool> ActorExistsAsync(Guid id);
        Task<ActorDto> GetActorAsync(Guid id, bool includeFilms = false, bool trackChanges = false);
        Task<IEnumerable<ActorDto>> GetActorsAsync(ActorParameters parameters, bool trackChanges = false);
        Task<IEnumerable<ActorDto>> GetActorsFromMovieAsync(Guid movieId, bool trackChanges = false);


    }
}
