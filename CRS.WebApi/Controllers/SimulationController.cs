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
using CRS.WebApi.Services;

namespace CRS.WebApi.Controllers
{
    [Route("api/simulation")]
    [ApiController]
    public class SimluationController(UnitOfWork unitOfWork, PersonaService personaService) : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        private readonly PersonaService _personaService = personaService;

        // POST: api/simulation/startNewSimulation
        [SwaggerOperation(Summary = "API endpoint to start a new simulation")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost("startNewSimulation")]
        public async Task<IActionResult> StartNewSimulation(StartSimulationRequest startSimulationRequest)
        {
            var simulation = await _unitOfWork.SimulationRepository.GetLatestSimulation();

            if (startSimulationRequest.Action == Data.Action.start || simulation == null) {
                simulation = new Simulation
                {
                    StartTime = DateTime.Parse(startSimulationRequest.StartTime!),
                };

                _unitOfWork.SimulationRepository.Create(simulation);
            }

            _unitOfWork.TaxPaymentRepository.DeleteAll();
            _unitOfWork.TaxPayerRepository.DeleteAll();

            var personas = await _personaService.GetPersonaList();

            if (startSimulationRequest.Action == Data.Action.start)
            {
                foreach (var persona in personas)
                {
                    if (persona.Adult)
                    {
                        _unitOfWork.TaxPayerRepository.Create(new TaxPayer
                        {
                            PersonaId = persona.Id,
                            SimulationId = simulation.Id,
                            AmountOwing = 0,
                            Group = (int)Data.TaxPayerType.INDIVIDUAL,
                            Status = (int)Data.TaxStatus.INACTIVE
                        });
                    }
                }

                _unitOfWork.Save();
            }

            return NoContent();
        }
    }
}
