using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class CreateTransactionRequest 
{
    [JsonPropertyName("transactions")]
    public List<DebitOrder> Transactions { get; set; } = null!;
}
