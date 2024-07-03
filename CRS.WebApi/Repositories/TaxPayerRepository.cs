using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class TaxPayerRepository(CrsdbContext context) : GenericRepository<TaxPayer, long>(context)
{
    public async Task<TaxPayer?> GetByUUID(Guid uuid)
    {
        return (await Find(taxPayer => taxPayer.TaxPayerId == uuid)).FirstOrDefault();
    }

    public async Task<TaxPayer?> GetByName(string companyName)
    {
        return (await Find(taxPayer => taxPayer.Name == companyName)).FirstOrDefault();
    }

    public async Task<TaxPayer?> GetByPersonaId(long id)
    {
        return (await Find(taxPayer => taxPayer.PersonaId == id)).FirstOrDefault();
    }
}