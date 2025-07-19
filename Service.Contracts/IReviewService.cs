using Movies.Core.DTOs.ReviewDTOs;

namespace Service.Contracts
{
    public interface IReviewService
    {
        Task<bool> ReviewExistsAsync(Guid id);
        Task<ReviewDto> GetReviewAsync(Guid id, bool trackChanges = false);
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(bool trackChanges = false);
        Task<IEnumerable<ReviewDto>> GetReviewsFromMovieAsync(Guid movieId, bool trackChanges = false);


    }
}
