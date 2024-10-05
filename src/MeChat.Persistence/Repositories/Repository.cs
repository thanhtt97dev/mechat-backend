using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeChat.Persistence.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext context;

    public Repository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Add(TEntity entity)
    {
        context.Add(entity);
    }

    public virtual async void Dispose()
    {
        await context.DisposeAsync();
    }
    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public void RemoveMultiple(List<TEntity> entities)
    {
        context.Set<TEntity>().RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public void UpdateMultiple(List<TEntity> entities)
    {
        context.Set<TEntity>().UpdateRange(entities);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        IQueryable<TEntity> items = context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);
        bool result = await items.AnyAsync(predicate, cancellationToken);
        return result;
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object?>>[] includeProperties)
    {
        IQueryable<TEntity> items = context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties)
    {
        var result = await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);
        return result;
    }
}
