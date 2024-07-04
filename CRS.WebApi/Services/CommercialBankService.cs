using CRS.WebApi.Data;
using Microsoft.Net.Http.Headers;

public class CommercialBankService
{
    private readonly HttpClient _httpClient;

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
}