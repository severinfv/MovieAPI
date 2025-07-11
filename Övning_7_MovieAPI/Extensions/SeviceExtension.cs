using Microsoft.EntityFrameworkCore;

namespace _Movies.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("ApplicationDataContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));
        }
    }
}
