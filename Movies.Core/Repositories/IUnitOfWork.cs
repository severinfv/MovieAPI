namespace Movies.Core.Repositories;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }
    IActorRepository ActorRepository { get; }
    IReviewRepository ReviewRepository { get; }
    Task CompleteAsync();
}