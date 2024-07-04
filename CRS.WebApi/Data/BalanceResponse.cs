using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class Balance
{
    [JsonPropertyName("accountbalance")]
    public decimal AccountBalance { get; set; }
}


public class BalanceResponse 
{
    [JsonPropertyName("data")]
    public Balance Balance { get; set; } = null!;
}
