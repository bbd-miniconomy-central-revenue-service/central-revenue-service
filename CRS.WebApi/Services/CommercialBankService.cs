using CRS.WebApi.Data;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace CRS.WebApi.Services;

public class CommercialBankService
{
    private readonly HttpClient _httpClient;
    private readonly string _accountName = "central-revenue-service";

    public CommercialBankService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.commercialbank.projects.bbdgrad.com/");

        httpClient.DefaultRequestHeaders.Add("X-Origin", "central_revenue");
    }

    public async Task<TranscactionsResponse?> GetTransactions() =>
        await _httpClient.GetFromJsonAsync<TranscactionsResponse>("transcations");

    public async Task<Transaction?> GetTransactionByRef(string reference)
    {
        var transcations = (await GetTransactions())?.Data.Transactions;
        var transactionByRef = transcations?.FirstOrDefault(transaction => transaction.Reference == reference);

        return transactionByRef;
    }

    public async Task CreateDebitOrder(string businessName, decimal amount)
    {
        HttpRequestMessage request = new(HttpMethod.Post, "debitOrder/create");

        var debitOrder = new DebitOrder
        {
            DebitAccountName = _accountName,
            CreditAccountName = businessName,
            DebitRef = _accountName,
            CreditRef = businessName,
            Amount = amount
        };

        List<DebitOrder> debitOrders = [debitOrder];

        var debitOrderRequest = new CreateDebitOrderRequest
        {
            DebitOrders = debitOrders
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(debitOrderRequest), System.Text.Encoding.UTF8, "application/json");

        await _httpClient.SendAsync(request);
    }

    public async Task MakePayment(string businessName, decimal? amount = null)
    {
        var balance = await QueryBalance();

        if (balance == null || balance == 0 || amount > balance) return;

        HttpRequestMessage request = new(HttpMethod.Post, "transactions/create");

        var transaction = new DebitOrder
        {
            DebitAccountName = _accountName,
            CreditAccountName = businessName,
            DebitRef = _accountName,
            CreditRef = businessName,
            Amount = (decimal)(amount ?? balance)
        };

        List<DebitOrder> transactions = [transaction];

        var transactionRequest = new CreateTransactionRequest
        {
            Transactions = transactions
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(transactionRequest), System.Text.Encoding.UTF8, "application/json");

        await _httpClient.SendAsync(request);
    }

    public async Task<decimal?> QueryBalance()
    {
        var balanceResponse = await _httpClient.GetFromJsonAsync<BalanceResponse>("account/balance");

        return balanceResponse?.Balance.AccountBalance;
    }
}