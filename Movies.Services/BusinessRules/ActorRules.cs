using Movies.Core.Exceptions;

namespace Movies.Services.BusinessRules;
public class ActorRules
{
    public void EnsureUniqueActorInMovie(bool actorAlreadyAssigned)
    {
        if (actorAlreadyAssigned)
            throw new BusinessRuleException("An actor cannot be assigned to the same movie twice.");
    }
}
