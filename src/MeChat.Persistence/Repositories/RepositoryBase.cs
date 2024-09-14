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

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includeProperties)
    {
        return await FindAll(null, includeProperties).AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }
        
}
