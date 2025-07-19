namespace Movies.Core.Exceptions;

public class ActorNotFoundException : NotFoundException
{
    public ActorNotFoundException(Guid id) : base($"Actor with id {id} was not found") { }
}
