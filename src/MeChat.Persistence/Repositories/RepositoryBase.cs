using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeChat.Persistence.Repositories;
public class RepositoryBase<TEntity, TKey> : Repository<TEntity>, IRepositoryBase<TEntity, TKey>  where TEntity : EntityBase<TKey>
{
    public RepositoryBase(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
    {
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8603 // Possible null reference return.
    }
        

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
    {
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.
        return await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
    }
}
