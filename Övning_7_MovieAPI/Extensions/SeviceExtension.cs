using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Repositories;
using Movies.Services;
using Service.Contracts;

namespace Movies.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("ApplicationDataContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped(provider => new Lazy<IMovieRepository>(() => provider.GetRequiredService<IMovieRepository>()));
            services.AddScoped(provider => new Lazy<IActorRepository>(() => provider.GetRequiredService<IActorRepository>()));
            services.AddScoped(provider => new Lazy<IReviewRepository>(() => provider.GetRequiredService<IReviewRepository>()));
        }

        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped(provider => new Lazy<IMovieService>(() => provider.GetRequiredService<IMovieService>()));
            services.AddScoped(provider => new Lazy<IActorService>(() => provider.GetRequiredService<IActorService>()));
            services.AddScoped(provider => new Lazy<IReviewService>(() => provider.GetRequiredService<IReviewService>()));
        }
    } 
}
