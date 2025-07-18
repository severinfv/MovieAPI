using Domain.Models.Entities;
namespace Domain.Contracts.Repositories
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<bool> ExistsAsync(Guid id);
        Task<List<Review>> GetAllAsync(bool trackChanges = false);
        Task<Review?> GetByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId, bool trackChanges);

    }
}