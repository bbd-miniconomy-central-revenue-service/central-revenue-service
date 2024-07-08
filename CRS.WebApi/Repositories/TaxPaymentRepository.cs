using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class TaxPaymentRepository(CrsdbContext context) : GenericRepository<TaxPayment, long>(context)
{
    public async Task<TaxPayment?> GetByTaxPayerId(long taxPayerId)
    {
        return (await Find(taxPayment => taxPayment.TaxPayerId == taxPayerId)).FirstOrDefault();
    }

    public async Task<List<TaxPayment>> GetUnsettledPayments()
    {
        return (await Find(taxPayment => taxPayment.Settled == false)).ToList();
    }

    public async Task<List<TaxPayment>> GetUnsettledPaymentsByTaxPayerId(long taxPayerId)
    {
        return (await Find(taxPayment => taxPayment.TaxPayerId == taxPayerId && taxPayment.Settled == false)).ToList();
    }

    public override void DeleteAll()
    {
        _context.TaxPayments.ExecuteDeleteAsync();
    }
}