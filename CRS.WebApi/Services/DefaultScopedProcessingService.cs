using CRS.WebApi.Repositories;
using System.Text.Json;
using CRS.WebApi.Data;
using CRS.WebApi.Models;

namespace CRS.WebApi.Services;

public sealed class DefaultScopedProcessingService(
    ILogger<DefaultScopedProcessingService> logger, 
    UnitOfWork unitOfWork,
    HandOfZeusService handOfZeusService,
    PersonaService personaService,
    CommercialBankService commercialBakingService) : IScopedProcessingService
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly HandOfZeusService _handOfZeusService = handOfZeusService;
    private readonly PersonaService _personaService = personaService;
    private readonly CommercialBankService _commercialBankService = commercialBakingService;
    private readonly int _minute = 60_0000;
    private int _minuteCounter = 0;

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        await UpdateSimulation();

        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateTaxRates();

            _minuteCounter += 2;

            await Task.Delay(_minute * 2, stoppingToken);

            if (_minuteCounter == 60)
            {
                logger.LogInformation(
                    "{ServiceName} settling payments",
                    nameof(DefaultScopedProcessingService));

                await SettlePayments();

                await _commercialBankService.MakePayment("labour-broker");

                _minuteCounter = 0;
            }
        }
    }

    public async Task UpdateTaxRates()
    {
        var taxRateResponse = await _handOfZeusService.GetTaxRates();

        if (taxRateResponse != null)
        {
            var vat = await _unitOfWork.TaxTypeRepository.GetById((int)Data.TaxType.VAT);
            var income = await _unitOfWork.TaxTypeRepository.GetById((int)Data.TaxType.INCOME);
            var property = await _unitOfWork.TaxTypeRepository.GetById((int)Data.TaxType.PROPERTY);

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
            var latestSimulation = await _unitOfWork.SimulationRepository.GetLatestSimulation();

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

    public async Task SettlePayments()
    {
        var taxpayersInDebt = await _unitOfWork.TaxPayerRepository.GetOwingTaxPayers();
        var surplusTaxpayers = await _unitOfWork.TaxPayerRepository.GetSurplusTaxPayers();

        foreach (var taxpayer in taxpayersInDebt)
        {
            if (taxpayer.Group == (int)Data.TaxPayerType.BUSINESS)
            {
                await _commercialBankService.CreateDebitOrder(taxpayer.Name!.Replace("_", "-"), taxpayer.AmountOwing);
            }
        }

        foreach (var taxpayer in surplusTaxpayers)
        {
            if (taxpayer.Group == (int)Data.TaxPayerType.BUSINESS)
            {
                await _commercialBankService.MakePayment(taxpayer.Name!.Replace("_", "-"), -taxpayer.AmountOwing);
            }
        }
    }
}