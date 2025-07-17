namespace Domain.Models.Exceptions;

public class ReviewNotFoundException : NotFoundException
{
    public ReviewNotFoundException(int id) : base($"Review with id {id} was not found") { }
}