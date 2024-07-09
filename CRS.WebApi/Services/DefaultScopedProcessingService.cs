using CRS.WebApi.Repositories;
using System.Text.Json;
using CRS.WebApi.Data;
using CRS.WebApi.Models;

namespace CRS.WebApi.Services;

public sealed class DefaultScopedProcessingService : IScopedProcessingService
{
    private readonly ILogger<DefaultScopedProcessingService> logger;
    private readonly UnitOfWork _unitOfWork;
    private readonly HandOfZeusService _handOfZeusService;
    private readonly IPersonaService _personaService;
    private readonly CommercialBankService _commercialBankService;
    private DateTime _currentTimestamp;
    private DateTime _startTime;

    public DefaultScopedProcessingService(IServiceScopeFactory serviceScopeFactory)
    {
        IServiceScope scope = serviceScopeFactory.CreateScope();
        logger = scope.ServiceProvider.GetRequiredService<ILogger<DefaultScopedProcessingService>>();
        _unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
        _handOfZeusService = scope.ServiceProvider.GetRequiredService<HandOfZeusService>();
        _personaService = scope.ServiceProvider.GetRequiredService<IPersonaService>();
        _commercialBankService = scope.ServiceProvider.GetRequiredService<CommercialBankService>();
    }
 
    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        await UpdateSimulation();

        await UpdateTaxRates();

        _currentTimestamp = DateTime.UtcNow;

        while (!stoppingToken.IsCancellationRequested)
        {
            if ((long)_currentTimestamp.Subtract(_startTime).TotalMinutes % 2 == 0 && 
                (long)_currentTimestamp.Subtract(_startTime).TotalMinutes >= 2)
            {
                logger.LogInformation(
                    "{ServiceName} settling payments",
                    nameof(DefaultScopedProcessingService));

                await UpdateTaxRates();

                await SettlePayments();

                _commercialBankService.MakePayment("labour-broker");

                _startTime = _currentTimestamp;
            }

            _currentTimestamp = DateTime.UtcNow;
        }
    }

    public async Task UpdateTaxRates()
    {
        var taxRateResponse = await _handOfZeusService.GetTaxRates();

        if (taxRateResponse != null)
        {
            var vat = _unitOfWork.TaxTypeRepository.GetById((int)Data.TaxType.VAT);
            var income = _unitOfWork.TaxTypeRepository.GetById((int)Data.TaxType.INCOME);
            var property = _unitOfWork.TaxTypeRepository.GetById((int)Data.TaxType.PROPERTY);

            if (taxRateResponse.Vat != null) vat!.Rate = (int)taxRateResponse.Vat;
            if (taxRateResponse.Income != null) income!.Rate = (int)taxRateResponse.Income;
            if (taxRateResponse.Property != null) property!.Rate = (int)taxRateResponse.Property;

            _unitOfWork.TaxTypeRepository.Update(vat!);
            _unitOfWork.TaxTypeRepository.Update(income!);
            _unitOfWork.TaxTypeRepository.Update(property!);
        }
    }

    public async Task UpdateSimulation()
    {
        DateTime? startTime = await GetStartTime();

        if (startTime != null)
        {
            var latestSimulation = _unitOfWork.SimulationRepository.GetLatestSimulation();

            if (latestSimulation == null || DateTime.Compare((DateTime)startTime, latestSimulation.StartTime) > 0)
            {
                logger.LogInformation(
                    "{ServiceName} starting new simulation",
                    nameof(DefaultScopedProcessingService));

                var simulation = new Simulation
                {
                    StartTime = (DateTime)startTime
                };

                _unitOfWork.SimulationRepository.Create(simulation);
                _unitOfWork.Save();

                _unitOfWork.TaxPaymentRepository.DeleteAll();
                _unitOfWork.TaxPayerRepository.DeleteAll();

                var personas = await _personaService.GetPersonaList();

                foreach (var persona in personas)
                {
                    if (persona.Adult)
                    {
                        _unitOfWork.TaxPayerRepository.Create(new TaxPayer
                        {
                            PersonaId = persona.Id,
                            SimulationId = simulation.Id,
                            AmountOwing = 0,
                            Group = (int)Data.TaxPayerType.INDIVIDUAL,
                            Status = (int)Data.TaxStatus.INACTIVE
                        });
                    }
                }

                _unitOfWork.Save();

                _startTime = simulation.StartTime;
            }
        }
    }

    public async Task<DateTime?> GetStartTime()
    {
        var startTimeResponse = await _handOfZeusService.GetStartTime();

        if (startTimeResponse != null && startTimeResponse.StartTime != null)
        {
            return DateTime.Parse(startTimeResponse.StartTime);
        }

        return default;
    }

    public async Task UpdateTaxPayerBalance(List<TaxPayer> taxpayers)
    {
        foreach (var taxpayer in taxpayers)
        {
            if (taxpayer.Group == (int)Data.TaxPayerType.BUSINESS)
            {
                var result = _commercialBankService.CreateDebitOrder(taxpayer.Name!.Replace("_", "-"), taxpayer.AmountOwing);

                if (result.Result == "success")
                {
                    var taxPayment = (await _unitOfWork.TaxPaymentRepository.GetUnsettledPayments()).FirstOrDefault();
                    
                    taxpayer.AmountOwing = 0;
                    taxpayer.Updated = DateTime.UtcNow;

                    _unitOfWork.TaxPayerRepository.Update(taxpayer);

                    if (taxPayment != null)
                    {
                        taxPayment!.Settled = true;
                        _unitOfWork.TaxPaymentRepository.Update(taxPayment);
                    }
                }
            }
        }
    }
    public async Task SettlePayments()
    {
        var taxpayersInDebt = await _unitOfWork.TaxPayerRepository.GetOwingTaxPayers();
        var surplusTaxpayers = await _unitOfWork.TaxPayerRepository.GetSurplusTaxPayers();

        await UpdateTaxPayerBalance(taxpayersInDebt);
        await UpdateTaxPayerBalance(surplusTaxpayers);

        _unitOfWork.Save();
    }
}