using System.Text.Json.Serialization;

namespace CRS.WebApi;

public class TaxRate
{
    [JsonPropertyName("rate")]
    public int Rate { get; set; }
}
