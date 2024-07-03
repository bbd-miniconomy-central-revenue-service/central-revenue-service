namespace CRS.WebApi.Services;

using CRS.WebApi.Data;
using CRS.WebApi.Models;
public class PaymentService
{
    private readonly CrsdbContext _context;

    public PaymentService(CrsdbContext context)
    {
        _context = context;
    }

    public int CreatePayment(TaxInvoiceRequest taxInvoiceRequest)
    {
        var taxPayment = new TaxPayment
        {
            TaxPayerId = 138,  
            Amount = taxInvoiceRequest.Amount,
            TaxType = (int)taxInvoiceRequest.TaxType,
            Created = DateTime.UtcNow 
        };

        _context.TaxPayments.Add(taxPayment);
        _context.SaveChanges();

        return taxPayment.Id;
    }


}
