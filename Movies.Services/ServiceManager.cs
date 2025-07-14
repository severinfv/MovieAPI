using Service.Contracts;

namespace Movies.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<IMovieService> movieService;
    private Lazy<IActorService> actorService;
    public IMovieService MovieService => movieService.Value;
    public IActorService ActorService => actorService.Value;

    public ServiceManager(Lazy<IMovieService> movieService, Lazy<IActorService> actorService)
    {
        this.movieService = movieService;
        this.actorService = actorService;
    }

}
