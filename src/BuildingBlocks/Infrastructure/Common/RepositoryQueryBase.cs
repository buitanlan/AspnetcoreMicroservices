using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class RepositoryQueryBase<T , TK, TContext>(TContext context) : IRepositoryQueryBase<T, TK, TContext>
    where T : EntityBase<TK>
    where TContext : DbContext
{
    public IQueryable<T> FindAll(bool trackChanges = false)
        => !trackChanges ? context.Set<T>().AsNoTracking() : context.Set<T>();

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindAll(trackChanges);

        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
        => !trackChanges ? context.Set<T>().Where(expression).AsNoTracking() : context.Set<T>().Where(expression);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public async Task<T?> GetByIdAsync(TK id)
        => await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();


    public async Task<T?> GetByIdAsync(TK id, params Expression<Func<T, object>>[] includeProperties)
        => await FindByCondition(x => x.Id.Equals(id), false,includeProperties).FirstOrDefaultAsync();

    public async Task<TK> CreateAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity.Id;
    }
}
