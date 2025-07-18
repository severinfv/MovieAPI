using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Context;

namespace Movies.Infrastructure.Repositories;

public class DirectorRepository : RepositoryBase<Director>//, IDirectorRepository
{
    public DirectorRepository(ApplicationDbContext context) : base(context) { }
    public async Task<bool> AnyAsync(Guid id) => await DbSet.AnyAsync(d => d.Id == id);

    public async Task<IEnumerable<Director>> GetDirectorAsync(Guid directorId, bool trackChanges = false)
    {
        return await FindByCondition(a => a.Movies.Any(m => m.Id == directorId), trackChanges).ToListAsync();
    }
}
