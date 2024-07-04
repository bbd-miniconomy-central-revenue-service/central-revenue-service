using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using CRS.WebApi.Models;
using CRS.WebApi.Data;
using CRS.WebApi.Services;
using CRS.WebApi.Repositories;

namespace CRS.WebApi.Controllers
{
    [Route("api/taxpayment")]
    [ApiController]
    public class TaxPaymentController(
        TaxCalculatorService taxCalculator, 
        UnitOfWork unitOfWork, 
        PaymentVerificationService paymentVerificationService) : ControllerBase
    {
        private readonly TaxCalculatorService _taxCalculator = taxCalculator;
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        private readonly PaymentVerificationService _paymentVerificationService = paymentVerificationService;

        // POST: api/taxPayment/createTaxInvoice
        [SwaggerOperation(Summary = "Creates a tax payment record and sends back an invoice")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxInvoice))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [HttpPost("createTaxInvoice")]
        public async Task<IActionResult> CreateTaxInvoice(TaxInvoiceRequest taxInvoiceRequest)
        {
            try
            {
                var tax = await _taxCalculator.CalculateTax(taxInvoiceRequest.Amount, taxInvoiceRequest.TaxType.ToString());

                var taxpayer = await _unitOfWork.TaxPayerRepository.GetByUUID(taxInvoiceRequest.TaxId);
                
                if (taxpayer != null)
                {
                    var taxPayment = new TaxPayment
                    {
                        TaxPayerId = taxpayer.Id,  
                        Amount = tax,
                        TaxType = (int)taxInvoiceRequest.TaxType,
                        Settled = false
                    };

                    _unitOfWork.TaxPaymentRepository.Create(taxPayment);
                    _unitOfWork.Save();

                    return Ok(new TaxInvoice
                    {
                        PaymentId = taxPayment.Id,
                        AmountDue = tax,
                    });

                }
                else
                {  
                    return StatusCode(StatusCodes.Status404NotFound, "Taxpayer not found");
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating tax invoice: " + ex.Message);
            }
        }

        // POST: api/taxPayment/submitNoticeOfPayment
        [SwaggerOperation(Summary = "Verifies that payment has been made and updates the corresponding payment record. A VerificationRequest is sent to the callback URL with the results.")]
        [SwaggerResponse(StatusCodes.Status202Accepted, Type = typeof(NoticeOfPaymentResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost("submitNoticeOfPayment")]
        public IActionResult SubmitNoticeOfPayment(NoticeOfPaymentRequest noticeOfPaymentRequest)
        {
            _ = _paymentVerificationService.UpdatePayment(noticeOfPaymentRequest);
            return StatusCode(StatusCodes.Status202Accepted, "Processing request");
        }
    }
}
