using CRS.WebApi.Data;
using CRS.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRS.WebApi.Controllers
{
    [Route("api/records")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RecordsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/records/history
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("/history")]
        public async Task<ActionResult<TaxRecordsResponse>> GetTaxHistory()
        {
            try 
            {
                var history = await _unitOfWork.TaxRecordViewRepository.GetRecords();
                
                if (history != null)
                {
                    return Ok(
                        history.Select(tr => new TaxRecordsResponse
                        {
                            Id = tr.TaxId,
                            Type = Enum.GetName(typeof(TaxPayerType), tr.TaxPayerType),
                            HasPaid = tr.HasPaid,
                            AmountOwing = tr.AmountOwing,
                            TaxType = tr.TaxType

                        }).ToList()
                    );

                }
                else
                {
                    return NotFound("No records found");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating tax invoice: " + ex.Message);
            }
            
        }

    }

}


