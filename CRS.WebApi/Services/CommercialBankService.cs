using CRS.WebApi.Data;
using CRS.WebApi.Repositories;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace CRS.WebApi.Services;

public class CommercialBankService
{
    private readonly HttpClient _httpClient;
    private readonly UnitOfWork _unitOfWork;

    public CommercialBankService(HttpClient httpClient, UnitOfWork unitOfWork)
    {
        _httpClient = httpClient;
        _unitOfWork = unitOfWork;

        _httpClient.BaseAddress = new Uri("https://api.commercialbank.projects.bbdgrad.com/");

        httpClient.DefaultRequestHeaders.Add("X-Origin", "central_revenue");
    }

    public async Task<TranscactionsResponse?> GetTransactions()
    {
        List<Transaction> transactions = _unitOfWork.TaxPaymentRepository.All().Select(
            payment =>
            {
                var taxpayer = _unitOfWork.TaxPayerRepository.GetById(payment.TaxPayerId);

                return new Transaction
                {
                    Id = Guid.NewGuid().ToString(),
                    Reference = taxpayer!.TaxPayerId.ToString(),
                    Amount = 2000,
                    Status = "success"
                };
                
            }
        ).ToList();

        return await Task.Run(() =>
        {
            return new TranscactionsResponse
            {
                Data = new TranscationsData
                {
                    Transactions = transactions
                }
            };
        });
    }
        

    public async Task<Transaction?> GetTransactionByRef(string reference)
    {
        var transcations = (await GetTransactions())?.Data.Transactions;
        var transactionByRef = transcations?.FirstOrDefault(transaction => transaction.Reference == reference);

        return transactionByRef;
    }

    public PaymentResponse CreateDebitOrder(string businessName, decimal amount)
    {
        return new PaymentResponse
        {
            Result = "success"
        };
    }

    public PaymentResponse MakePayment(string businessName, decimal? amount = null)
    {
        return new PaymentResponse
        {
            Result = "success"
        };
    }

    public async Task<decimal?> QueryBalance()
    {
        return await Task.Run(() =>
        {
            var balanceResponse = new BalanceResponse
            {
                Balance = new Balance
                {
                    AccountBalance = 1000000
                }
            };
            return balanceResponse?.Balance.AccountBalance;
        });
    }
}