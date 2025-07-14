using Domain.Contracts.Repositories;
using Movies.Infrastructure.Data;

namespace Movies.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<IMovieRepository> movieRepository;
    private readonly Lazy<IActorRepository> actorRepository;
    public IMovieRepository MovieRepository => movieRepository.Value;
    public IActorRepository ActorRepository => actorRepository.Value;

    private readonly ApplicationDbContext context;
    public UnitOfWork(
        ApplicationDbContext context,
        Lazy<IMovieRepository> movieRepository,
        Lazy<IActorRepository> actorRepository)
    {
        this.movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        this.actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task CompleteAsync() => await context.SaveChangesAsync();

}
