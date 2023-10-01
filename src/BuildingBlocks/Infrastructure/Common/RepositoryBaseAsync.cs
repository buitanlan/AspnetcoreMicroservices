using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common;

public class RepositoryBaseAsync<T, TK, TContext>(TContext context, IUnitOfWork<TContext> unitOfWork)
    : RepositoryQueryBase<T, TK, TContext>(context),IRepositoryBaseAsync<T, TK, TContext>
    where T : EntityBase<TK>
    where TContext : DbContext
{

    public async Task<IList<TK>> CreateListAsync(IEnumerable<T> entities)
    {
        await context.Set<T>().AddRangeAsync(entities);
        return entities.Select(x => x.Id).ToList();
    }

    public Task UpdateAsync(T entity)
    {
        if (context.Entry(entity).State == EntityState.Unchanged)
            return Task.CompletedTask;
        var exist = context.Set<T>().Find(entity.Id);
        context.Entry(exist).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
    }

    public async Task UpdateListAsync(IEnumerable<T> entities) => await context.Set<T>().AddRangeAsync(entities);

    public Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteListAsync(IEnumerable<T> entities)
    {
        context.Set<T>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task<int> SaveChangesAsync() => unitOfWork.CommitAsync();

    public Task<IDbContextTransaction> BeginTransactionAsync() => context.Database.BeginTransactionAsync();

    public async Task EndTransactionAsync()
    {
        await SaveChangesAsync();
        await context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync() => await context.Database.RollbackTransactionAsync();

}
