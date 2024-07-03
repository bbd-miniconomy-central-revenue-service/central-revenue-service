using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class TaxPaymentRepository(CrsdbContext context, ILogger logger) : GenericRepository<TaxPayment, long>(context, logger)
{
    public async Task<IEnumerable<TaxPayment>> GetByTaxPayerId(long taxPayerId)
    {
        return await Find(taxPayment => taxPayment.TaxPayerId == taxPayerId);
    }
}