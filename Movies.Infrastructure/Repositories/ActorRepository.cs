using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;

namespace Movies.Infrastructure.Repositories;

public class ActorRepository : RepositoryBase<Actor>, IActorRepository
{
    public ActorRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Actor>> GetActorsAsync(int movieId, bool trackChanges = false)
    {
        return await FindByCondition(a => a.Movies.Any(m => m.Id == movieId), trackChanges).ToListAsync();
    }
}
