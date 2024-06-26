using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public class TaxStatementResponse 
    {
        [JsonPropertyName("amountOwing")]
        public int AmountOwing {  get; set; }
    }
}
