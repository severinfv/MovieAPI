namespace Domain.Models.Exceptions;

public class ActorNotFoundException : NotFoundException
{
    public ActorNotFoundException(int id) : base($"Actor with id {id} was not found") { }
}
