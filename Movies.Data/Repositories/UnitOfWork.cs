using Movies.Core.Repositories;
using Movies.Data.Context;

namespace Movies.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<IMovieRepository> movieRepository;
    private readonly Lazy<IActorRepository> actorRepository;
    private readonly Lazy<IReviewRepository> reviewRepository;
    public IMovieRepository MovieRepository => movieRepository.Value;
    public IActorRepository ActorRepository => actorRepository.Value;
    public IReviewRepository ReviewRepository => reviewRepository.Value;

    private readonly ApplicationDbContext context;
    public UnitOfWork(
        ApplicationDbContext context,
        Lazy<IMovieRepository> movieRepository,
        Lazy<IActorRepository> actorRepository,
        Lazy<IReviewRepository> reviewRepository)
    {
        this.movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        this.actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
        this.reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task CompleteAsync() => await context.SaveChangesAsync();

}
