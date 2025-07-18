using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.BusinessRules;
internal class MovieRules
{
    public void EnsureMaxAcceptedReviews(int reviewCount, DateOnly year)
    {
        var movieAge = DateTime.Now.Year - year.Year;

        if (movieAge >= 20 && reviewCount >= 5)
            throw new Exception("Movies older than 20 years can have a maximum of 5 review.");

        if (reviewCount >= 10)
            throw new Exception("A movie can have a maximum of 10 reviews.");
    }
    public void EnsureNonNegativeBudget(decimal budget)
    {
        if (budget < 0)
            throw new Exception("Budget cannot be negative.");
    }

    public void EnsureDocumentaryRule(int actorCount, decimal budget)
    {
        if (actorCount > 10)
            throw new Exception("A documentary movie can not have more than 10 actors");

        if (budget > 1000000)
            throw new Exception("Budget can not exceed a million.");
    }

}
