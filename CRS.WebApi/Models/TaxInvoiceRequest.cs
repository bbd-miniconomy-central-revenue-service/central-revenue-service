using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRS.WebApi.Models
{
    public class TaxInvoiceRequest
    {
        [Required]
        [JsonPropertyName("taxId")]
        public required Guid TaxId { get; set; }

        [Required]
        [JsonPropertyName("taxType")]
        public required string TaxType { get; set; }

        [Required]
        [JsonPropertyName("amount")]
        public required decimal Amount { get; set; }

    }
}
