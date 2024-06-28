using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public enum TaxType 
    {
        INCOME,
        VAT,
        PROPERTY
    }
    public class TaxInvoiceRequest
    {
        [Required]
        [JsonPropertyName("taxId")]
        public required Guid TaxId { get; set; }

        [Required]
        [JsonPropertyName("taxType")]
        [EnumDataType(typeof(TaxType))]
        public required TaxType TaxType { get; set; }

        [Required]
        [JsonPropertyName("amount")]
        public required decimal Amount { get; set; }
    }
}
