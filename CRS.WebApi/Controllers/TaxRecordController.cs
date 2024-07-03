using CRS.WebApi.Data;
using CRS.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRS.WebApi.Controllers

{
    [Route("api/taxrecords")]
    [ApiController]
    public class TaxRecordController : ControllerBase
    {
    private readonly TaxRecordService _taxRecordService;

    public TaxRecordController(TaxRecordService taxRecordService)
    {
        _taxRecordService = taxRecordService;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        var taxRecords = _taxRecordService.GetTaxRecords();
        return Ok(new {taxRecords});
    }
}

}