using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions;

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
}
