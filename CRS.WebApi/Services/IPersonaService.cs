using CRS.WebApi.Data;
using Microsoft.Net.Http.Headers;

namespace CRS.WebApi.Services;

public interface IPersonaService
{
    public Task<PersonaListResponse?> GetPersonas();

    public Task<List<Persona>> GetPersonaList();
}