using MeChat.Common.Abstractions.Messages.DomainEvents;

namespace MeChat.Common.UseCases.V1.Notification;
public class Command
{
    public record ReadNotification(Guid Id) : ICommand;
    public record ReadAllNotification(Guid Id) : ICommand;
}
