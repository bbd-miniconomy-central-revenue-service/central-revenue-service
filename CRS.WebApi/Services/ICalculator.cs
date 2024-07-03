namespace CRS.WebApi.Services
{
    public interface ICalculator
    {
        decimal CalculateTax(decimal amount, decimal rate);
        decimal CalculateTaxWithRateFromDb(decimal amount, int taxTypeId);
    }
}

