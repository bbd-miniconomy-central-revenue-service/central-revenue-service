using CRS.WebApi.Models;
using CRS.WebApi.Repositories;

namespace CRS.WebApi.Repositories;

public class TaxRecordViewRepository(CrsdbContext context) : GenericRepository<TaxRecord, long>(context)
{
    public IEnumerable<TaxRecord> GetRecords()
    {
        return All();
    }
}
