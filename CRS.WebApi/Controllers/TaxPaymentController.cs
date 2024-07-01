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

namespace CRS.WebApi.Controllers
{
    [Route("api/taxpayment")]
    [ApiController]
    public class TaxPaymentController : ControllerBase
    {
        private readonly CrsdbContext _context;

        public TaxPaymentController(CrsdbContext context)
        {
            _context = context;
        }

        // POST: api/taxPayment/createTaxInvoice
        [SwaggerOperation(Summary = "Creates a tax payment record and sends back an invoice")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxInvoice))]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost("createTaxInvoice")]
        public IActionResult CreateTaxInvoice(TaxInvoiceRequest taxInvoiceRequest)
        {
            return Ok(
                new TaxInvoice
                {
                    PaymentId = 2,
                    AmountDue = 1200,
                    DueTime = new DueTime {
                        Days = 10,
                        Hours = 2,
                        Minutes = 2,
                        Seconds = 2
                    }
                });
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
