using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using CRS.WebApi.Models;

namespace CRS.WebApi.Controllers
{
    [Route("api/taxpayer")]
    [ApiController]
    public class TaxPayerController : ControllerBase
    {
        private readonly CrsdbContext _context;

        public TaxPayerController(CrsdbContext context)
        {
            _context = context;
        }

        // GET: api/taxPayer/persona/getTaxId
        [SwaggerOperation(Summary = "Gets the taxID for a persona")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [HttpGet("persona/getTaxId/{personaId}")]
        public ActionResult<TaxIdResponse> GetPersonaTaxId(int personaId)
        {
            return Ok(
                new TaxIdResponse
                {
                    TaxId = Guid.NewGuid()
                }
           );
        }

        // GET: api/taxPayer/business/getTaxNUmber
        [SwaggerOperation(Summary = "Gets the taxID for a business.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [HttpGet("business/getTaxId/{businessName}")]
        public ActionResult<TaxIdResponse> GetBusinessTaxId(string businessName)
        {
            return Ok(
                new TaxIdResponse
                {
                    TaxId = Guid.NewGuid()
                }
           );
        }

        // POST: api/taxPayer/business/register
        [SwaggerOperation(Summary = "Registers a business and assigns it a tax number.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxIdResponse))]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [HttpPost("business/register")]
        public ActionResult<TaxIdResponse> RegisterBusiness(RegisterBusinessRequest registerBusinessRequest)
        {
            return Ok(
                new TaxIdResponse
                {
                    TaxId = Guid.NewGuid()
                }
           );
        }

        // GET: api/taxPayer/getTaxStatement
        [SwaggerOperation(Summary = "Gets the tax statment for a tax payer")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxStatementResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [HttpGet("getTaxStatement/{taxId}")]
        public ActionResult<TaxStatementResponse> GetTaxStatement(Guid taxId)
        {
            return Ok(
                new TaxStatementResponse
                {
                    AmountOwing = 0
                }
            );
        }
    }
}
