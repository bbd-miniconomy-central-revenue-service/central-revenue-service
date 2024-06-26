using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public class DueTime
    {
        [JsonPropertyName("days")]
        public uint Days { get; set; }

        [JsonPropertyName("hours")]
        public uint Hours { get; set; }

        [JsonPropertyName("minutes")]
        public uint Minutes { get; set; }

        [JsonPropertyName("seconds")]
        public uint Seconds { get; set; }
    }

    public class TaxInvoice
    {
        [JsonPropertyName("paymentId")]
        public int PaymentId { get; set; }

        [JsonPropertyName("amountDue")]
        public decimal AmountDue { get; set; }

        [JsonPropertyName("dueTime")]
        public DueTime DueTime { get; set; } = new DueTime();

    }
}
