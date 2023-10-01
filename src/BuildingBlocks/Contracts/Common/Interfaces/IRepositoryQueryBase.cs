using System.Linq.Expressions;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;

namespace Contracts.Common.Interfaces;

public interface IRepositoryQueryBase<T, K, TContext>
    where T: EntityBase<K>
    where TContext : DbContext
{
    IQueryable<T> FindAll(bool trackChanges = false);
    IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties);

    Task<T?> GetByIdAsync(K id);
    Task<T?> GetByIdAsync(K id,  params Expression<Func<T, object>>[] includeProperties);
}
