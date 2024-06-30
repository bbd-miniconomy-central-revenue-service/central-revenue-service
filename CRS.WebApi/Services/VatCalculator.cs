namespace CRS.WebApi.Services;

public class VatCalculator : ICalculator
{
    public decimal CalculateTax(decimal amount, double rate)
    {
        return amount * (decimal)rate / 100;
    }
}
