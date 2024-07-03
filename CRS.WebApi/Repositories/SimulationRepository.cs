using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class SimulationRepository(CrsdbContext context) : GenericRepository<Simulation, int>(context) 
{
    public async Task<Simulation?> GetLatestSimulation()
    {
        var allRecords = await All();
        return allRecords.LastOrDefault();
    }
}