using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public class TaxInvoice
    {
        [JsonPropertyName("paymentId")]
        public long PaymentId { get; set; }

        [JsonPropertyName("amountDue")]
        public decimal AmountDue { get; set; }
    }
}
