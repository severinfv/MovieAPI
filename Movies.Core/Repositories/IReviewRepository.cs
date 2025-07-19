using Movies.Core.Entities;

namespace Movies.Core.Repositories
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<bool> ExistsAsync(Guid id);
        Task<List<Review>> GetAllAsync(bool trackChanges = false);
        Task<Review?> GetByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId, bool trackChanges);

    }
}