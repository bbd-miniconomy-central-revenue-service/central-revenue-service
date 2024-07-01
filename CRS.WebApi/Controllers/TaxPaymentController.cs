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

namespace CRS.WebApi.Controllers
{
    [Route("api/taxpayment")]
    [ApiController]
    public class TaxPaymentController : ControllerBase
    {
        private readonly CrsdbContext _context;
        private readonly TaxCalculatorService _taxCalculator;
        private readonly PaymentService _paymentService;

        public TaxPaymentController(CrsdbContext context, TaxCalculatorService taxCalculator, PaymentService paymentService)
        {
            _context = context;
            _taxCalculator = taxCalculator;
            _paymentService = paymentService;
        }

        // POST: api/taxPayment/createTaxInvoice
        [SwaggerOperation(Summary = "Creates a tax payment record and sends back an invoice")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxInvoice))]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost("createTaxInvoice")]
        public IActionResult CreateTaxInvoice(TaxInvoiceRequest taxInvoiceRequest)
        {
            try
            {
                var tax = _taxCalculator.CalculateTax(taxInvoiceRequest.Amount, taxInvoiceRequest.TaxType.ToString());

                var paymentId = _paymentService.CreatePayment(taxInvoiceRequest);

                var taxInvoice = new TaxInvoice
                {
                    PaymentId = paymentId,
                    AmountDue = tax,
                    DueTime = new DueTime
                    {
                        Days = 10,
                        Hours = 2,
                        Minutes = 2,
                        Seconds = 2
                    }
                };

                return Ok(taxInvoice);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating tax invoice: " + ex.Message);
            }
        }

        // POST: api/taxPayment/submitNoticeOfPayment
        [SwaggerOperation(Summary = "Verifies that payment has been made and updates the corresponding payment record. A VerificationRequest is sent to the callback URL with the results.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(NoticeOfPaymentResponse))]
        [HttpPost("submitNoticeOfPayment")]
        public IActionResult SubmitNoticeOfPayment(NoticeOfPaymentRequest noticeOfPaymentRequest)
        {
            return Ok(
                new NoticeOfPaymentResponse
                {
                    Result = "success"
                });
        }
    }
}
