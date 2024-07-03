using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public abstract class GenericRepository<T, K>(
    CrsdbContext context) : IRepository<T, K> where T : class
{   
    protected CrsdbContext _context = context;
    internal DbSet<T> dbSet = context.Set<T>();

    public virtual async Task<IEnumerable<T>> All() => await dbSet.ToListAsync();

   public virtual async void Create(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        _context.Remove(entity);
    }

    public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate) => await dbSet.Where(predicate).ToListAsync();

    public async Task<T?> GetById(K id) => await dbSet.FindAsync(id);

    public virtual void Update(T entity)
    {
        dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}