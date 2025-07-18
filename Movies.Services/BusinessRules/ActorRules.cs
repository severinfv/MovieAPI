using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.BusinessRules;
public class ActorRules
{
    public void EnsureUniqueActorInMovie(bool actorAlreadyAssigned)
    {
        if (actorAlreadyAssigned)
            throw new Exception("An actor cannot be assigned to the same movie twice.");
    }
}
