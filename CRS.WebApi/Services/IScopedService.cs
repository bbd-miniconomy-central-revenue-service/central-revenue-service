namespace CRS.WebApi.Services;

public interface IScopedProcessingService
{
    Task DoWorkAsync(CancellationToken stoppingToken);
}