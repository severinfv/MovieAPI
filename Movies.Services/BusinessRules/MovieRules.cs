using Movies.Core.Exceptions;

namespace Movies.Services.BusinessRules;
internal class MovieRules
{
    public void EnsureMaxAcceptedReviews(int reviewCount, DateOnly year)
    {
        var movieAge = DateTime.Now.Year - year.Year;

        if (movieAge >= 20 && reviewCount >= 5)
            throw new BusinessRuleException("Movies older than 20 years can have a maximum of 5 review.");

        if (reviewCount >= 10)
            throw new BusinessRuleException("A movie can have a maximum of 10 reviews.");
    }
    public void EnsureNonNegativeBudget(decimal budget)
    {
        if (budget < 0)
            throw new BusinessRuleException("Budget cannot be negative.");
    }

    public void EnsureDocumentaryRule(int actorCount, decimal budget)
    {
        if (actorCount > 10)
            throw new BusinessRuleException("A documentary movie can not have more than 10 actors");

        if (budget > 1000000)
            throw new BusinessRuleException("Budget can not exceed a million.");
    }

}
