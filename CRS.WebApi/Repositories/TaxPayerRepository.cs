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

    public override void DeleteAll()
    {
        _context.TaxPayers.ExecuteDeleteAsync();
    }
}