using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Notification;
using Microsoft.EntityFrameworkCore;

namespace MeChat.Application.UseCases.V1.Notification.CommandHandlers;
public class ReadAllNotificationCommandHandler : ICommandHandler<Command.ReadAllNotification>
{
    private readonly IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository;

    public ReadAllNotificationCommandHandler(IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository)
    {
        this.notificationRepository = notificationRepository;
    }

    public async Task<Result> Handle(Command.ReadAllNotification request, CancellationToken cancellationToken)
    {
        var notificationsUnRead = await notificationRepository.FindAll(x => x.UserId == request.Id && x.IsReaded == false).ToListAsync();

        if (notificationsUnRead.Count == 0)
            return Result.Success();

        notificationsUnRead.ForEach(x => { x.IsReaded = true; });
        notificationRepository.UpdateMultiple(notificationsUnRead);

        return Result.Success();
    }
}
