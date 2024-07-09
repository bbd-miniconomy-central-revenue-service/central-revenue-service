using CRS.WebApi.Repositories;
using System.Text.Json;
using CRS.WebApi.Data;
using CRS.WebApi.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace CRS.WebApi.Services;

public sealed class PaymentVerificationService(
    UnitOfWork unitOfWork,
    CommercialBankService commercialBankService,
    ILogger<PaymentVerificationService> logger)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly CommercialBankService _commercialBankService = commercialBankService;

    public async Task UpdatePayment(NoticeOfPaymentRequest noticeOfPaymentRequest)
    {
        
        var (payment, amountShort, taxpayer) = await VerifyPayment(noticeOfPaymentRequest.TaxId.ToString(), 
            noticeOfPaymentRequest.PaymentId);

        if (payment != null) 
        {
            _unitOfWork.TaxPaymentRepository.Update(payment);
            
            if (amountShort > 0) taxpayer!.AmountOwing = amountShort;

            taxpayer!.Updated = DateTime.UtcNow;

            _unitOfWork.TaxPayerRepository.Update(taxpayer!);

            _unitOfWork.Save();

            if (noticeOfPaymentRequest.CallbackURL != null)
            {
                if (amountShort > 0)
                {
                    await SendVerificationResults(noticeOfPaymentRequest.CallbackURL,
                        noticeOfPaymentRequest.PaymentId, amountShort, "failed");
                }
                else
                {

                    await SendVerificationResults(noticeOfPaymentRequest.CallbackURL,
                        noticeOfPaymentRequest.PaymentId, amountShort, "success");
                }
            }
        } else
        {
            if (noticeOfPaymentRequest.CallbackURL != null)
            {
                await SendVerificationResults(noticeOfPaymentRequest.CallbackURL,
                    noticeOfPaymentRequest.PaymentId, amountShort, "failed");
            }
        }
    }

    public async Task SendVerificationResults(string callbackUrl, int paymentId, decimal amountShort, string result)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, callbackUrl);
        request.Headers.Add("X-Origin", "central_revenue");

        string requestBody = JsonConvert.SerializeObject(new VerificationRequest
        {
            PaymentId = paymentId,
            AmountShort = amountShort,
            Result = result
        });

        request.Content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

        var httpClient = new HttpClient();

        await httpClient.SendAsync(request);
    }

    public async Task<Tuple<TaxPayment?, decimal, TaxPayer?>> VerifyPayment(string taxId, int paymentId)
    {
        logger.LogInformation(
            "{ServiceName} verifying payments",
            nameof(PaymentVerificationService)); 

        var transaction = await _commercialBankService.GetTransactionByRef(taxId);

        if (transaction == null) return Tuple.Create<TaxPayment?, decimal, TaxPayer?>(default, 0, default);

        var payment = _unitOfWork.TaxPaymentRepository.GetById(paymentId);

        if (payment == null) return Tuple.Create<TaxPayment?, decimal, TaxPayer?>(default, 0, default);

        var taxpayer = _unitOfWork.TaxPayerRepository.GetById(payment.TaxPayerId);

        if (transaction.Amount >= payment.Amount)
        {
            payment.Settled = true;

            // In case of a surplus
            taxpayer!.AmountOwing -= (transaction.Amount - payment.Amount);

            return Tuple.Create<TaxPayment?, decimal, TaxPayer?>(payment, 0, taxpayer);
        }

        // Check for credits
        var diff = payment.Amount - (transaction.Amount + (-taxpayer!.AmountOwing));

        return Tuple.Create<TaxPayment?, decimal, TaxPayer?>(payment, diff, taxpayer);
    }

}