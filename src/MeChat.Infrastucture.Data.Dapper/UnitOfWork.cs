using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Infrastucture.Dapper.Repositories;
using MeChat.Infrastucture.Data.Dapper.Repositories;

namespace MeChat.Infrastucture.Dapper;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public IUserRepository Users { get; private set; }

    public IUserSocialRepository UsersSocials { get; private set; }

    public INotificationRepository Notifications { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
        Users = new UserRepository(context);
        UsersSocials = new UserSocialRepository(context);
        Notifications = new NotificationRepository(context);
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken)
        => context.BeginTransactionAsync(cancellationToken);

    public Task CommitTransactionAsync(CancellationToken cancellationToken)
        => context.CommitTransactionAsync(cancellationToken);

    public Task RollbackTransactionAsync(CancellationToken cancellationToken)
        => context.RollbackTransactionAsync(cancellationToken);

    public async ValueTask DisposeAsync()
        => await context.DisposeAsync();
}
