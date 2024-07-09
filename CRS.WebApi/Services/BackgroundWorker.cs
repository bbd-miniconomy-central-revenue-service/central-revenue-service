using System.Threading;
using System.Threading.Tasks;
using CRS.WebApi.Models;
using CRS.WebApi.Repositories;
using CRS.WebApi.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class BackgroundWorker(IServiceScopeFactory serviceScopeFactory, ILogger<BackgroundWorker> logger) : BackgroundService
{
    private const string ClassName = nameof(BackgroundWorker);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "{Name} is running.", ClassName);

        await DoWorkAsync(cancellationToken);
    }

    public async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "{Name} is working.", ClassName);

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IScopedProcessingService scopedProcessingService =
            scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

        await scopedProcessingService.DoWorkAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "{Name} is stopping.", ClassName);

        await base.StopAsync(cancellationToken);
    }
}
