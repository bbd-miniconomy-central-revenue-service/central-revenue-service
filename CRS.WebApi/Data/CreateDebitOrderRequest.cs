using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public class DebitOrder
{
    [JsonPropertyName("debitAccountName")]
    public string DebitAccountName { get; set; } = null!;

    [JsonPropertyName("creditAccountName")]
    public string CreditAccountName { get; set; } = null!;

    [JsonPropertyName("creditRef")]
    public string CreditRef { get; set; } = null!;

    [JsonPropertyName("debitRef")]
    public string DebitRef { get; set; } = null!;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}

public class CreateDebitOrderRequest 
{
    [JsonPropertyName("debitOrders")]
    public List<DebitOrder> DebitOrders { get; set; } = null!;
}
