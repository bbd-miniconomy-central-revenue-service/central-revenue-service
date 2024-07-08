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

    public async Task<TaxRateResponse?> GetTaxRates() 
    {
        try 
        {
            return await _httpClient.GetFromJsonAsync<TaxRateResponse>("tax-rate");
        }
        catch (Exception) 
        {
            return new TaxRateResponse
            {
                Income = 10,
                Vat = 12,
                Property = 20
            };
        }
        
    }
    

    public async Task<StartSimulationResponse?> GetStartTime()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<StartSimulationResponse>("start-time");
        }
        catch(Exception)
        {
            return new StartSimulationResponse
            {
                StartTime = "2024-07-05T07:33:16"
            };
        }
    }
        
}