using CRS.WebApi.Data;
using Microsoft.Net.Http.Headers;

public class PersonaService
{
    private readonly HttpClient _httpClient;

    public PersonaService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.persona.projects.bbdgrad.com");

        httpClient.DefaultRequestHeaders.Add("X-Origin", "central_revenue");
    }

    public async Task<PersonaListResponse?> GetPersonas() =>
        await _httpClient.GetFromJsonAsync<PersonaListResponse>("api/Persona/getAllPersonas");

    public async Task<List<Persona>> GetPersonaList()
    {
        var personaListResponse = await GetPersonas();

        if (personaListResponse != null)
        {
            return personaListResponse.Data.Personas;
        }

        return [];
    }
}