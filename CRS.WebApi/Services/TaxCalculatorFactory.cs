namespace CRS.WebApi.Services;
using CRS.WebApi.Models;
using CRS.WebApi.Repositories;

public class TaxCalculatorFactory
{
    private readonly UnitOfWork _unitOfWork;
    public TaxCalculatorFactory(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ICalculator<decimal, int> GetTaxCalculator(string taxType)
    {
        return taxType.ToLower() switch
        {
            "property" => new PropertyTaxCalculator(_unitOfWork),
            "income" => new IncomeTaxCalculator(_unitOfWork),
            "vat" => new VatCalculator(_unitOfWork),
            _ => throw new ArgumentException("Invalid tax type"),
        };
    }

}
