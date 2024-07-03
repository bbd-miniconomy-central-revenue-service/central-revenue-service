namespace CRS.WebApi.Services
{
    using CRS.WebApi.Models;
    public class VatCalculator : ICalculator
    {
        private readonly CrsdbContext _context;

        public VatCalculator(CrsdbContext context)
        {
            _context = context;
        }

        public decimal CalculateTax(decimal amount, decimal rate)
        {
            return amount * rate;
        }

        public decimal CalculateTaxWithRateFromDb(decimal amount, int taxTypeId)
        {
            var taxType = _context.TaxTypes.Find(taxTypeId);
            return taxType == null ? throw new Exception("TaxType not found.") : CalculateTax(amount, taxType.Rate);
        }
    }
}
