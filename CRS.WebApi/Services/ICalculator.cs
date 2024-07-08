namespace CRS.WebApi.Services
{
    public interface ICalculator<T, K>
    {
        T CalculateTax(T amount, K rate);
        T CalculateTaxWithRateFromDb(T amount, int taxTypeId);
    }
}

