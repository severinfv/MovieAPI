namespace Domain.Models.Exceptions;

public class ReviewNotFoundException : NotFoundException
{
    public ReviewNotFoundException(Guid id) : base($"Review with id {id} was not found") { }
}