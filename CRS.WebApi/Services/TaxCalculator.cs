namespace CRS.WebApi.Services;

public class TaxCalculator
{
    private readonly decimal  taxRate = 0.16m;
    private static readonly Lazy<TaxCalculator> _lazyInstance = new Lazy<TaxCalculator>(() => new TaxCalculator());

        private TaxCalculator() { }

        public static TaxCalculator Instance
        {
            get
            {
                return _lazyInstance.Value;
            }
        }

        public decimal CalculateTax(decimal amount, string taxType)
        {
            return amount * taxRate;
        }
}
