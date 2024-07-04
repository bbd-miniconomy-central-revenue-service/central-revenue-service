using CRS.WebApi.Repositories;
using System.Text.Json;
using CRS.WebApi.Data;
using CRS.WebApi.Models;

namespace CRS.WebApi.Services;

public sealed class VerificationBackgroundService(
    ILogger<VerificationBackgroundService> logger, 
    UnitOfWork unitOfWork,
    HandOfZeusService commercialBankService) : IScopedProcessingService
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly CommercialBankService _commercialBankService;
    private readonly int _month = 60_0000 * 60;

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(_month, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(_month, stoppingToken);
        }
    }



}