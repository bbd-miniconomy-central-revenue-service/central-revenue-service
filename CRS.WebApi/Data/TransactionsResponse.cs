using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class Item
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("reference")]
    public string Reference { get; set; } = null!;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = null!;
}

public class TranscationsData
{
    [JsonPropertyName("items")]
    public List<Item> Items { get; set; } = [];
}

public class TranscactionsResponse
{
    [JsonPropertyName("data")]
    public TranscationsData Data { get; set; } = null!;
}
