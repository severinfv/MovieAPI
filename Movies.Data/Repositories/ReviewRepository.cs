﻿using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using Movies.Data.Context;

namespace Movies.Data.Repositories;

public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context) { }
    public async Task<bool> ExistsAsync(Guid id) => await EntityExistsAsync(id);
    public async Task<List<Review>> GetAllAsync(bool trackChanges = false) => await FindAll(trackChanges).ToListAsync();
    public async Task<Review?> GetByIdAsync(Guid id, bool trackChanges = false)
    {
        IQueryable<Review> query = FindByCondition(m => m.Id == id, trackChanges);
        //query = query.Include(m => m.Movies);

        return await query.FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId, bool trackChanges = false)
        => await FindByCondition(r => r.MovieId == movieId, trackChanges).ToListAsync();



}
