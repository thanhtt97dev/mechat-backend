namespace MeChat.Common.Abstractions.Data.EntityFramework;
public interface IUnitOfWork : IAsyncDisposable
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);

    Task SaveChangeUserTrackingAsync(Guid userId, CancellationToken cancellationToken = default);
}
