using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class TaxRateResponse
{
    [JsonPropertyName("vat")]
    public decimal? Vat { get; set; }

    [JsonPropertyName("income")]
    public decimal? Income { get; set; }

    [JsonPropertyName("business")]
    public decimal? Property { get; set; }
}
