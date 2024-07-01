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
        switch (taxType.ToLower())
        {
            case "property":
                return new PropertyTaxCalculator(_context);
            case "income":
                return new IncomeTaxCalculator(_context);
            case "vat":
                return new VatCalculator(_context);
            default:
                throw new ArgumentException("Invalid tax type");
        }
    }

}
