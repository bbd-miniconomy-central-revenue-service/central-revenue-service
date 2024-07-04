using CRS.WebApi.Data;
using Microsoft.Net.Http.Headers;

namespace CRS.WebApi.Services;

public class HandOfZeusService
{
    private readonly HttpClient _httpClient;

    public HandOfZeusService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.zeus.projects.bbdgrad.com");

        httpClient.DefaultRequestHeaders.Add("X-Origin", "central_revenue");
    }

    public async Task<TaxRateResponse?> GetTaxRates() =>
        await _httpClient.GetFromJsonAsync<TaxRateResponse>("tax-rate");

    public async Task<StartSimulationResponse?> GetStartTime() =>
        await _httpClient.GetFromJsonAsync<StartSimulationResponse>("start-time");
}