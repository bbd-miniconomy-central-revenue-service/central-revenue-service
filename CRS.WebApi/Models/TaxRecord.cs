namespace CRS.WebApi;

public class TaxRecord
{
    public Guid TaxId { get; set; }
    public int TaxPayerType { get; set; }
    public int HasPaid { get; set; }
    public decimal AmountOwing { get; set; }
    public string? TaxType { get; set; }
}
