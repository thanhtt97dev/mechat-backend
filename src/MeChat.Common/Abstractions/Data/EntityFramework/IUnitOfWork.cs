using System.Runtime.InteropServices;

namespace MeChat.Common.Abstractions.Data.EntityFramework;
public interface IUnitOfWork : IAsyncDisposable
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);

    Task SaveChangeUserTrackingAsync(Guid userId, CancellationToken cancellationToken = default);

    Task BeginTransactionAsync([Optional]CancellationToken cancellationToken);
    Task CommitTransactionAsync([Optional] CancellationToken cancellationToken);
    Task RollbackTransactionAsync([Optional] CancellationToken cancellationToken);
}
