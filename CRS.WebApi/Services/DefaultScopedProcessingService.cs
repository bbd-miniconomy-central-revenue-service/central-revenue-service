using CRS.WebApi.Repositories;

namespace CRS.WebApi.Services;

public sealed class DefaultScopedProcessingService(
    ILogger<DefaultScopedProcessingService> logger, UnitOfWork unitOfWork) : IScopedProcessingService
{
    private int _executionCount;
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ++_executionCount;

            logger.LogInformation(
                "{ServiceName} working, execution count: {Count}",
                nameof(DefaultScopedProcessingService),
                _executionCount);

            await Task.Delay(10_000, stoppingToken);
        }
    }
}