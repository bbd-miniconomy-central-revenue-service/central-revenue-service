namespace CRS.WebApi.Services
{
    using CRS.WebApi.Models;
    using CRS.WebApi.Repositories;

    public class PropertyTaxCalculator : ICalculator<decimal, int>
    {
        private readonly UnitOfWork _unitOfWork;

        public PropertyTaxCalculator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public decimal CalculateTax(decimal amount, int rate) => amount* rate * (decimal)0.01;

        public decimal CalculateTaxWithRateFromDb(decimal amount, int taxTypeId)
        {
            var taxType = _unitOfWork.TaxTypeRepository.GetById(taxTypeId);
            return taxType == null ? throw new Exception("TaxType not found.") : CalculateTax(amount, taxType.Rate);
        }
    }
}