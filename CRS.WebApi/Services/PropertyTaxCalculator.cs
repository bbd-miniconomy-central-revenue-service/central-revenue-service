namespace CRS.WebApi.Services;

public class PropertyTaxCalculator : ICalculator
{
    public decimal CalculateTax(decimal amount, double rate)
    {
        return amount * (decimal)rate / 100;
    }
}
