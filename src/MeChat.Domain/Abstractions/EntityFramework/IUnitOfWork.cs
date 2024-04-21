namespace MeChat.Domain.Abstractions.EntityFramework;
public interface IUnitOfWork : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
