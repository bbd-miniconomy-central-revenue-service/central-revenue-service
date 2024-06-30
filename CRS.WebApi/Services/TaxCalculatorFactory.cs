namespace CRS.WebApi.Services;

public static class TaxCalculatorFactory
{
    public static ICalculator GetTaxCalculator(string taxType)
    {
        switch (taxType.ToLower())
        {
            case "property":
                return new PropertyTaxCalculator();
            case "income":
                return new IncomeTaxCalculator();
            case "vat":
                return new VatCalculator();
            default:
                throw new ArgumentException("Invalid tax type");
        }
    }

}
