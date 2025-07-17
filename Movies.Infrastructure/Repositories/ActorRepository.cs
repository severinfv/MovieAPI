using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Movies.Infrastructure.Repositories;

public class ActorRepository : RepositoryBase<Actor>, IActorRepository
{
    public ActorRepository(ApplicationDbContext context) : base(context) { }
    public async Task<bool> ExistsAsync(int id) => await base.EntityExistsAsync(id);
    public async Task<List<Actor>> GetAllAsync(bool trackChanges = false) => await FindAll(trackChanges).ToListAsync();
    public async Task<Actor?> GetByIdAsync(int id, bool trackChanges = false)
    {
        IQueryable<Actor> query = FindByCondition(m => m.Id == id, trackChanges);
        query = query.Include(m => m.Movies);

        return await query.FirstOrDefaultAsync();
    }

   
    public async Task<IEnumerable<Actor>> GetActorsByMovieIdAsync(int movieId, bool trackChanges = false) 
        => await FindByCondition(a => a.Movies.Any(m => m.Id == movieId), trackChanges).ToListAsync();
    


}
