using CRS.WebApi.Repositories;
using System.Text.Json;
using CRS.WebApi.Data;
using CRS.WebApi.Models;

namespace CRS.WebApi.Services;

public sealed class DefaultScopedProcessingService(
    ILogger<DefaultScopedProcessingService> logger, UnitOfWork unitOfWork,
    HandOfZeusService handOfZeusService) : IScopedProcessingService
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly HandOfZeusService _handOfZeusService = handOfZeusService;

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            DateTime? startTime = await GetStartTime();

            if (startTime != null)
            {
                var latestSimulation = await _unitOfWork.SimulationRepository.GetLatestSimulation();

                if (DateTime.Compare((DateTime)startTime, latestSimulation.StartTime) > 0)
                {
                    logger.LogInformation(
                        "{ServiceName} starting new simulation",
                        nameof(DefaultScopedProcessingService));

                    _unitOfWork.SimulationRepository.Create(
                        new Simulation
                        {
                            StartTime = (DateTime)startTime,
                        });
                }
            }

            await Task.Delay(10_000, stoppingToken);
        }
    }

    public async Task<DateTime?> GetStartTime()
    {
        var startTimeResponse = await _handOfZeusService.GetStartTime();

        if (startTimeResponse != null)
        {
            return DateTime.Parse(startTimeResponse.StartTime);
        }

        return default;
    }
}