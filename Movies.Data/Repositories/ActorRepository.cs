﻿using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Parameters;
using Movies.Core.Repositories;
using Movies.Data.Context;

namespace Movies.Data.Repositories;

public class ActorRepository : RepositoryBase<Actor>, IActorRepository
{
    public ActorRepository(ApplicationDbContext context) : base(context) { }
    public async Task<bool> ExistsAsync(Guid id) => await EntityExistsAsync(id);
    public async Task<Actor?> GetByIdAsync(Guid id, bool trackChanges = false)
    {
        IQueryable<Actor> query = FindByCondition(m => m.Id == id, trackChanges);
        query = query.Include(m => m.Movies);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<PagedList<Actor>> GetAllAsync(ActorParameters parameters, bool trackChanges = false)
    {
        var query = FindAll(trackChanges);

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(m => m.Name.Contains(parameters.Name));

        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))

            query = query.Where(a =>
                a.Name.Contains(parameters.SearchQuery));

        return await PagedList<Actor>.PageAsync(query, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<IEnumerable<Actor>> GetActorsByMovieIdAsync(Guid movieId, bool trackChanges = false)
        => await FindByCondition(a => a.Movies.Any(m => m.Id == movieId), trackChanges).ToListAsync();



}
