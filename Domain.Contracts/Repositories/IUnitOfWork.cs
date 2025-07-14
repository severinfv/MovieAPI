namespace Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }
    Task CompleteAsync();
}