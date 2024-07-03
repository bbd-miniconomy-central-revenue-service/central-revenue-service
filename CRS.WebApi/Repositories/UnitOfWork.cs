using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class UnitOfWork(CrsdbContext context) : IDisposable  
{
    private readonly CrsdbContext _context = context;
    private TaxTypeRepository? _taxTypeRepository;
    private TaxPaymentRepository? _taxPaymentRepository;
    private TaxPayerRepository? _taxPayerRepository;
    private SimulationRepository? _simulationRepository;
    private UserRepository? _userRepository;

    private TaxRecordViewRepository? _taxRecordViewRepository;

    public TaxTypeRepository TaxTypeRepository
    {
        get
        {

            this._taxTypeRepository ??= new TaxTypeRepository(this._context);
            return _taxTypeRepository;
        }
    }

    public TaxPaymentRepository TaxPaymentRepository
    {
        get
        {

            this._taxPaymentRepository ??= new TaxPaymentRepository(this._context);
            return _taxPaymentRepository;
        }
    }

    public TaxPayerRepository TaxPayerRepository
    {
        get
        {

            this._taxPayerRepository ??= new TaxPayerRepository(this._context);
            return _taxPayerRepository;
        }
    }

    public SimulationRepository SimulationRepository
    {
        get
        {

            this._simulationRepository ??= new SimulationRepository(this._context);
            return _simulationRepository;
        }
    }

    public UserRepository UserRepository
    {
        get
        {

            this._userRepository ??= new UserRepository(this._context);
            return _userRepository;
        }
    }

    public TaxRecordViewRepository TaxRecordViewRepository
    {
        get
        {

            this._taxRecordViewRepository??= new TaxRecordViewRepository(this._context);
            return _taxRecordViewRepository;
        }
    }


    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}