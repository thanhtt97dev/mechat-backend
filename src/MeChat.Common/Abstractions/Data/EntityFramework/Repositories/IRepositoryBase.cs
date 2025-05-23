﻿using System.Linq.Expressions;

namespace MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
public interface IRepositoryBase<TEntity, TKey> : IRepository<TEntity>, IDisposable where TEntity : class
{
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties);
    Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties);
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties);
    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object?>>[] includeProperties);
}
