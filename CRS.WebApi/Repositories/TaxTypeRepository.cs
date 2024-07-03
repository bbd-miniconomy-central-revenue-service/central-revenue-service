using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class TaxTypeRepository(CrsdbContext context, ILogger logger) : GenericRepository<TaxType, int>(context, logger) { }