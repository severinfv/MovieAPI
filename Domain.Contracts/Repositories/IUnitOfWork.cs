using Domain.Contracts.Repositories;

namespace Movies.Infrastructure.Repositories;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }
    Task CompleteAsync();
}