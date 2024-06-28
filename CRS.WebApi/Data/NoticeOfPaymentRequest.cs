using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public class NoticeOfPaymentRequest
    {
        [Required]
        [JsonPropertyName("taxId")]
        public required Guid TaxId { get; set; }

        [Required]
        [JsonPropertyName("paymentId")]
        public required int PaymentId { get; set; }

        [Required]
        [JsonPropertyName("callbackURL")]
        public required string CallbackURL {  get; set; }
    }
}
