using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class SimulationRepository(CrsdbContext context) : GenericRepository<Simulation, int>(context) 
{
    public Simulation? GetLatestSimulation()
    {
        var allRecords = All();
        return allRecords.LastOrDefault();
    }
}