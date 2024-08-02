﻿using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeChat.Persistence.Repositories;
public class RepositoryBase<TEntity, TKey> : Repository<TEntity>, IRepositoryBase<TEntity, TKey>  where TEntity : EntityBase<TKey>
{
    public RepositoryBase(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties)
    {
        if(predicate == null) throw new ArgumentNullException(nameof(predicate));
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

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties)
    {
        return await FindAll(null, includeProperties).AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }
        

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties)
    {
        var result = await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);
        return result;
    }
}
