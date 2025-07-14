using Domain.Contracts.Repositories;
using Service.Contracts;

namespace Movies.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<IMovieService> movieService;
    public IMovieService MovieService => movieService.Value;
    //.. services
    public ServiceManager(IUnitOfWork uow) 
    { 
        movieService = new Lazy<IMovieService>(() => new MovieService(uow));
    }

}
