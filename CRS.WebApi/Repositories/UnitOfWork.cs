using CRS.WebApi.Models;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class UnitOfWork  
{
    private readonly CrsdbContext _context;
    private TaxTypeRepository? _taxTypeRepository;
    private TaxPaymentRepository? _taxPaymentRepository;
    private TaxPayerRepository? _taxPayerRepository;
    private SimulationRepository? _simulationRepository;
    private UserRepository? _userRepository;

    private TaxRecordViewRepository? _taxRecordViewRepository;

    public UnitOfWork(IServiceScopeFactory serviceScopeFactory)
    {
        IServiceScope scope = serviceScopeFactory.CreateScope();

        _context = scope.ServiceProvider.GetRequiredService<CrsdbContext>();
    }

    public TaxTypeRepository TaxTypeRepository
    {
        get
        {

            this._taxTypeRepository ??= new TaxTypeRepository(_context);
            return _taxTypeRepository;
        }
    }

    public TaxPaymentRepository TaxPaymentRepository
    {
        get
        {

            this._taxPaymentRepository ??= new TaxPaymentRepository(_context);
            return _taxPaymentRepository;
        }
    }

    public TaxPayerRepository TaxPayerRepository
    {
        get
        {

            this._taxPayerRepository ??= new TaxPayerRepository(_context);
            return _taxPayerRepository;
        }
    }

    public SimulationRepository SimulationRepository
    {
        get
        {

            this._simulationRepository ??= new SimulationRepository(_context);
            return _simulationRepository;
        }
    }

    public UserRepository UserRepository
    {
        get
        {

            this._userRepository ??= new UserRepository(_context);
            return _userRepository;
        }
    }

    public TaxRecordViewRepository TaxRecordViewRepository
    {
        get
        {

            this._taxRecordViewRepository??= new TaxRecordViewRepository(_context);
            return _taxRecordViewRepository;
        }
    }


    public void Save()
    {
        _context.SaveChanges();
    }
}