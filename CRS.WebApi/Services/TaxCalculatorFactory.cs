namespace CRS.WebApi.Services;
using CRS.WebApi.Models;
public class TaxCalculatorFactory
{
    private CrsdbContext _context;
    public TaxCalculatorFactory(CrsdbContext context)
    {
        _context = context;
    }

    public ICalculator GetTaxCalculator(string taxType)
    {
        return taxType.ToLower() switch
        {
            "property" => new PropertyTaxCalculator(_context),
            "income" => new IncomeTaxCalculator(_context),
            "vat" => new VatCalculator(_context),
            _ => throw new ArgumentException("Invalid tax type"),
        };
    }

}
