using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Context;
using System.Diagnostics;

namespace Movies.API.Extensions;

public class DataSeedHostingService : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DataSeedHostingService> logger;

    public DataSeedHostingService(IServiceProvider serviceProvider, ILogger<DataSeedHostingService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        if (!env.IsDevelopment()) return;

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (await context.Movies.AnyAsync(cancellationToken)) return;

        try
        {
            await ImdbDataSeed.InitAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError($"Data seed fail with error: {ex.Message}");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

}
