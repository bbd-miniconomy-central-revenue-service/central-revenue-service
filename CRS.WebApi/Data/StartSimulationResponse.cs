﻿using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class StartSimulationResponse
{
    [JsonPropertyName("startTime")]
    public string StartTime { get; set; } = null!;
}
