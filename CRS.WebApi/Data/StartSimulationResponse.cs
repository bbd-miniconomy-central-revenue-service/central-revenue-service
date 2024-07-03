using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class StartSimulationResponse
{
    [JsonPropertyName("start_date")]
    public string StartTime { get; set; } = null!;
}
