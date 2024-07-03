using System.Collections.Generic;
using CRS.WebApi.Models;  // Make sure this matches your namespace and class structure

namespace CRS.WebApi.Services
{
public class TaxRecordService
{

    private readonly CrsdbContext _context;

    public TaxRecordService(CrsdbContext context)
    {
         _context = context;
    }

    public List<TaxRecordViewModel> GetTaxRecords()
    {
        return _context.TaxRecordView.ToList();
    }
}
}

