namespace Movies.Core.DTOs.ReviewDTOs
{
    public record ReviewDto(Guid ApplicationUserId, string? ReviewText, double? UserRating);
}
