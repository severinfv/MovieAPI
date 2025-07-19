using Movies.Core.DTOs.ReviewDTOs;
using Movies.Core.Exceptions;

namespace Movies.Services.BusinessRules;
public class ReviewRules
{
    public void EnsureReviewHasValidContent(ReviewDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.ReviewText) && (!dto.UserRating.HasValue))
            throw new BusinessRuleException("A review must include either a comment, a rating, or both.");
    }
}