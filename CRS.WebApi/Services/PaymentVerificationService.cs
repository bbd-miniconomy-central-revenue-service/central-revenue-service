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
    CommercialBankService commercialBankService)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly CommercialBankService _commercialBankService = commercialBankService;

    public async Task UpdatePayment(NoticeOfPaymentRequest noticeOfPaymentRequest)
    {
        var (payment, amountShort) =  await VerifyPayment(noticeOfPaymentRequest.TaxId.ToString(), noticeOfPaymentRequest.PaymentId);

        if (payment != null) 
        {
            _unitOfWork.TaxPaymentRepository.Update(payment);
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
        }

        if (noticeOfPaymentRequest.CallbackURL != null)
        {
            await SendVerificationResults(noticeOfPaymentRequest.CallbackURL,
                noticeOfPaymentRequest.PaymentId, amountShort, "failed");
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

    public async Task<Tuple<TaxPayment?, decimal>> VerifyPayment(string taxId, int paymentId)
    {
        var transaction = await _commercialBankService.GetTransactionByRef(taxId);

        if (transaction == null) return Tuple.Create<TaxPayment?, decimal>(default, 0);

        var payment = await _unitOfWork.TaxPaymentRepository.GetById(paymentId);

        if (payment == null) return Tuple.Create<TaxPayment?, decimal>(default, 0);

        if (payment.Amount == transaction.Amount)
        {
            payment.Settled = true;
            return Tuple.Create<TaxPayment?, decimal>(payment, 0);
        }

        payment.Amount -= transaction.Amount;

        return Tuple.Create<TaxPayment?, decimal>(payment, payment.Amount - transaction.Amount);
    }

}