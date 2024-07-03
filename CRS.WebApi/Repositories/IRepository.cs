using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public interface IRepository<T, K> where T : class
{
    Task<IEnumerable<T>> All();
    Task<T?> GetById(K id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
}