namespace Service.Contracts
{
    public interface IServiceManager
    {
        IMovieService MovieService { get; }
    }
}