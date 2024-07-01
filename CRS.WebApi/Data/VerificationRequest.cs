using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public class VerificationRequest
    {
        [Required]
        [JsonPropertyName("result")]
        public string Result { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("amountShort")]
        public decimal AmountShort { get; set; }

        [Required]
        [JsonPropertyName("paymentId")]
        public required int PaymentId { get; set; }
    }
}
