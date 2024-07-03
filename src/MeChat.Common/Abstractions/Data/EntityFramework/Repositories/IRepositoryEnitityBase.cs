using System.Linq.Expressions;

namespace MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
public interface IRepositoryEnitityBase<TEntity, TKey> :IRepositoryBase<TEntity>, IDisposable where TEntity : class
{
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
}
