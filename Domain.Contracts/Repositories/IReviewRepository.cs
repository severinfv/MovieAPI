using Domain.Models.Entities;
namespace Domain.Contracts.Repositories
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<bool> ExistsAsync(int id);
        Task<List<Review>> GetAllAsync(bool trackChanges = false);
        Task<Review?> GetByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId, bool trackChanges);

    }
}