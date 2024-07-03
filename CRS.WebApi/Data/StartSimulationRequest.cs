using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public enum Action
{
    start,
    reset
}

public class StartSimulationRequest 
{
    [JsonPropertyName("action")]
    public Action Action { get; set; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; set; }
}
