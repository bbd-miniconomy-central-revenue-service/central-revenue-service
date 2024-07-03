using System.Threading;
using System.Threading.Tasks;
using CRS.WebApi.Models;
using CRS.WebApi.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class BackgroundWorker(UnitOfWork unitOfWork, ILogger<BackgroundWorker> logger) : IHostedService, IDisposable
{
    private readonly ILogger<BackgroundWorker> _logger = logger;
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private Timer? _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Background Worker is starting.");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _logger.LogInformation("Background Worker is doing work.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Background Worker is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
