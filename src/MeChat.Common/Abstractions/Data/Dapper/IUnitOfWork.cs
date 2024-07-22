using MeChat.Common.Abstractions.Data.Dapper.Repositories;

namespace MeChat.Common.Abstractions.Data.Dapper;
public interface IUnitOfWork : IAsyncDisposable
{
    public IUserRepository Users { get; }
    public IUserSocialRepository UsersSocials { get; }

    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
