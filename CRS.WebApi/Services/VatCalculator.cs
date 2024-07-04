namespace CRS.WebApi.Services
{
    using CRS.WebApi.Models;
    using CRS.WebApi.Repositories;

    public class VatCalculator : ICalculator<decimal, int>
    {
        private readonly UnitOfWork _unitOfWork;

        public VatCalculator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public decimal CalculateTax(decimal amount, int rate) => amount * rate * (decimal)0.01;

        public async Task<decimal> CalculateTaxWithRateFromDb(decimal amount, int taxTypeId)
        {
            var taxType = await _unitOfWork.TaxTypeRepository.GetById(taxTypeId);
            return taxType == null ? throw new Exception("TaxType not found.") : CalculateTax(amount, taxType.Rate);
        }
    }
}
