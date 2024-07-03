using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class SimulationRepository(CrsdbContext context, ILogger logger) : GenericRepository<Simulation, int>(context, logger) 
{
    public async Task<Simulation> GetLatestSimulation()
    {
        return (await All()).Last();
    }
}