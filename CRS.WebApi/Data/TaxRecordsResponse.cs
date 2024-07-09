using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class TaxRecordsResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("hasPaid")]
    public int HasPaid { get; set; }

    [JsonPropertyName("amountOwing")]
    public decimal AmountOwing { get; set; }

    [JsonPropertyName("taxType")]
    public string? TaxType { get; set; }

    [JsonPropertyName("paymentAmount")]
    public decimal PaymentAmount { get; set; }

}
