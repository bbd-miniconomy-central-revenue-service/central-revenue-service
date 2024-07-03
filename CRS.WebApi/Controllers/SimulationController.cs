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
    [Route("api/simulation")]
    [ApiController]
    public class SimluationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public SimluationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/simulation/startNewSimulation
        [SwaggerOperation(Summary = "API endpoint to start a new simulation")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost("startNewSimulation")]
        public IActionResult startNewSimulation(StartSimulationRequest startSimulationRequest)
        {
            if (startSimulationRequest.Action == Data.Action.start) {
                _unitOfWork.SimulationRepository.Create(
                    new Simulation
                    {
                        StartTime = DateTime.Parse(startSimulationRequest.StartTime!),
                    });
            } 
            else
            {
                // TODO: truncate all the tables
                return NoContent();
            }

            _unitOfWork.Save();

            return NoContent();
        }
    }
}
