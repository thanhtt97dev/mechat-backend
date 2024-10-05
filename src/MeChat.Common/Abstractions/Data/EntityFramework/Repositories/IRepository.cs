using System.Linq.Expressions;

namespace MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void UpdateMultiple(List<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveMultiple(List<TEntity> entities);
    Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties);
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties);
    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object?>>[] includeProperties);
}
