namespace CRS.WebApi.Services;

public class IncomeTaxCalculator : ICalculator
{
    public decimal CalculateTax(decimal amount, double rate)
    {
        return amount * (decimal)rate / 100;
    }
}
