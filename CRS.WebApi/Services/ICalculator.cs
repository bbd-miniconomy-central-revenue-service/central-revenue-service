namespace CRS.WebApi.Services
{
    public interface ICalculator<T, K>
    {
        T CalculateTax(T amount, K rate);
        Task<T> CalculateTaxWithRateFromDb(T amount, int taxTypeId);
    }
}

