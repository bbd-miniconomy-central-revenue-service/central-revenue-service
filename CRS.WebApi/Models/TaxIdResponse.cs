using System.Text.Json.Serialization;

namespace CRS.WebApi.Models
{
    public class TaxIdResponse 
    {
        [JsonPropertyName("taxId")]
        public Guid TaxId {  get; set; }
    }
}
