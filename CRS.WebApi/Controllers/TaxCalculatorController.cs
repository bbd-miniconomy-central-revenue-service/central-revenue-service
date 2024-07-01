using CRS.WebApi.data;
using CRS.WebApi.Data;
using CRS.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRS.WebApi.Controllers
{
    [Route("api/taxcalculator")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly TaxCalculatorService _taxCalculator;

        public TaxCalculatorController(TaxCalculatorService taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }
        // GET: api/taxcalculator/calculate
        [SwaggerOperation(Summary = "Calculates specified tax on a provided amount")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxAmountResponse))]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpGet("calculate")]
        public IActionResult CalculateTax(decimal amount, [FromQuery, SwaggerParameter(Description = "Type of tax", Required = true)]PossibleTaxTypes taxType)
        {
            var tax = _taxCalculator.CalculateTax(amount, taxType.ToString());
            return Ok(new { TaxableAmount = amount, TaxAmount = tax });
        }
    }   
}
