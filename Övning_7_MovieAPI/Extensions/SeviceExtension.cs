using Microsoft.EntityFrameworkCore;
using Övning_7_MovieAPI.Data;

namespace Övning_7_MovieAPI.Extensions
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
