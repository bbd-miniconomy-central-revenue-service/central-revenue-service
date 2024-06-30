namespace CRS.WebApi.Services;

public class TaxCalculator
{
    //TODO: get rates from zues endpoint
    private readonly double taxRate = 15;
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
            ICalculator calculator = TaxCalculatorFactory.GetTaxCalculator(taxType);
            return calculator.CalculateTax(amount, taxRate);
        }
}
