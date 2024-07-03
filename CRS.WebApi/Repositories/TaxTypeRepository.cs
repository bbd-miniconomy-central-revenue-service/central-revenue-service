using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class TaxTypeRepository(CrsdbContext context) : GenericRepository<TaxType, int>(context) 
{
    
}