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
using CRS.WebApi.Repositories;

namespace CRS.WebApi.Controllers
{
    [Route("api/taxpayer")]
    [ApiController]
    public class TaxPayerController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TaxPayerController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/taxPayer/persona/getTaxId
        [SwaggerOperation(Summary = "Gets the taxID for a persona")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [HttpGet("persona/getTaxId/{personaId}")]
        public async Task<ActionResult<TaxIdResponse>> GetPersonaTaxId(long personaId)
        {
            try
            {
                var taxpayer = await _unitOfWork.TaxPayerRepository.GetByPersonaId(personaId);

                if (taxpayer != null)
                {
                    return Ok(
                        new TaxIdResponse
                        {
                            TaxId = taxpayer.TaxPayerId
                        }
                    );

                }
                else
                {
                    return NotFound("Taxpayer not found");
                }
            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating tax invoice: " + ex.Message);
            }
            
        }

        // GET: api/taxPayer/business/getTaxNUmber
        [SwaggerOperation(Summary = "Gets the taxID for a business.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [HttpGet("business/getTaxId/{businessName}")]
        public async Task<ActionResult<TaxIdResponse>> GetBusinessTaxId(string businessName)
        {
            try
            {
                var taxpayer = await _unitOfWork.TaxPayerRepository.GetByName(businessName);

                if (taxpayer != null)
                {
                    return Ok(
                        new TaxIdResponse
                        {
                            TaxId = taxpayer.TaxPayerId                    
                        }
                    );

                }
                else
                {
                    return NotFound("Taxpayer not found");
                }
            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating tax invoice: " + ex.Message);
            }
        }

        // POST: api/taxPayer/business/register
        [SwaggerOperation(Summary = "Registers a business and assigns it a taxID.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxIdResponse))]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [HttpPost("business/register")]
        public async Task<ActionResult<TaxIdResponse>> RegisterBusiness(RegisterBusinessRequest registerBusinessRequest)
        {
            var latestSimulation = await _unitOfWork.SimulationRepository.GetLatestSimulation();

            if (latestSimulation == null)
            {
                return NoContent();
            }

            var newTaxPayer = new TaxPayer
            {
                Name = registerBusinessRequest.BusinessName,
                Group = (int)Data.TaxPayerType.BUSINESS,
                Status = (int)Data.TaxStatus.ACTIVE,
                AmountOwing = 0,
                SimulationId = latestSimulation.Id
            };
            
            _unitOfWork.TaxPayerRepository.Create(newTaxPayer);
            _unitOfWork.Save();

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
