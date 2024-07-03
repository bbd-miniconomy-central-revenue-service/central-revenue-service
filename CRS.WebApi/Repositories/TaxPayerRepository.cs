using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class TaxPayerRepository(CrsdbContext context, ILogger logger) : GenericRepository<TaxPayer, long>(context, logger)
{
    public async Task<TaxPayer?> GetByUUID(Guid uuid)
    {
        return (await Find(taxPayer => taxPayer.TaxPayerId == uuid)).FirstOrDefault();
    }
}