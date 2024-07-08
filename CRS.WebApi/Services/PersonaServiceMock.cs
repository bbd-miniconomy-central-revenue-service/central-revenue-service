using CRS.WebApi.Data;
using Microsoft.Net.Http.Headers;

namespace CRS.WebApi.Services;

public class PersonaServiceMock : IPersonaService
{
    private readonly HttpClient _httpClient;

    public PersonaServiceMock(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.persona.projects.bbdgrad.com");

        httpClient.DefaultRequestHeaders.Add("X-Origin", "central_revenue");
    }

    public async Task<PersonaListResponse?> GetPersonas()
    {
        List<Persona> personas = [
            new Persona {
                Id = 1,
                Alive = true,
                Adult = true,
            },
            new Persona {
                Id = 2,
                Alive = true,
                Adult = true,
            },
        ];

        return await Task.Run(() =>
        {
            return new PersonaListResponse
            {
                Data = new PersonaListData
                {
                    Personas = personas
                }
            };
        });
    }

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