namespace CRS.WebApi.Services
{
    public interface ICalculator
    {
        decimal CalculateTax(decimal amount, double rate);
    }
}

