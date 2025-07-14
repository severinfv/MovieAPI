using Movies.Infrastructure.Repositories;

namespace Movies.Services
{
    public class ServiceManager
    {
        private Lazy<MovieService> movieService;
        public MovieService MovieService => movieService.Value;
        //.. services
        public ServiceManager(IUnitOfWork uow) 
        { 
            movieService = new Lazy<MovieService>(() => new MovieService());
        }

    }
}
