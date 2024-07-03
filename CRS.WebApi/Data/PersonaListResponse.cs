﻿using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class Persona
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("alive")]
    public bool Alive { get; set; }

    [JsonPropertyName("adult")]
    public bool Adult { get; set; }
}

public class Data
{
    [JsonPropertyName("personas")]
    public List<Persona> Personas { get; set; } = [];
}

public class PersonaListResponse
{
    [JsonPropertyName("data")]
    public Data Data { get; set; } = null!;
}
