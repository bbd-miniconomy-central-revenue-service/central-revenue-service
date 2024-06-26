using System.Text.Json.Serialization;

namespace CRS.WebApi.Models
{
    public class TaxStatementResponse 
    {
        [JsonPropertyName("amountOwing")]
        public int AmountOwing {  get; set; }
    }
}
