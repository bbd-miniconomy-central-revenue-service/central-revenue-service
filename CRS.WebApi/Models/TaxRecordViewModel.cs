using System.Text.Json.Serialization;

namespace CRS.WebApi;

public class TaxRecordViewModel
{
    public string Id { get; set; }
    public string Type { get; set; }
    public bool HasPaid { get; set; }
    public decimal AmountOwing { get; set; }
    public string TaxType { get; set; }
}
