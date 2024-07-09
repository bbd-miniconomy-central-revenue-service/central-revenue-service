using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class PaymentResponse 
{
    [JsonPropertyName("result")]
    public string Result { get; set; } = null!;
}
