using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Response;

namespace MeChat.Common.UseCases.V1.Notification;
public class Query
{
    public record GetNotifications(string? Id, int PageIndex) : IQuery<PageResult<Response.Notification>>;
}
