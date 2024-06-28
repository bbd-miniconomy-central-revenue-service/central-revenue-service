using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public class TaxIdResponse 
    {
        [JsonPropertyName("taxId")]
        public Guid TaxId {  get; set; }
    }
}
