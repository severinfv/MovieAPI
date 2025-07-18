using Movies.Shared.DTOs.ReviewDTOs;

namespace Service.Contracts
{
    public interface IReviewService
    {
        Task<bool> ReviewExistsAsync(int id);
        Task<ReviewDto> GetReviewAsync(int id, bool trackChanges = false);
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(bool trackChanges = false);
        Task<IEnumerable<ReviewDto>> GetReviewsFromMovieAsync(int movieId, bool trackChanges = false);


    }
}
