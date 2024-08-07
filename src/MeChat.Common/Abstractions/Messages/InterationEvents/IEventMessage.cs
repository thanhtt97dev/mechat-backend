using MassTransit;
using MediatR;

namespace MeChat.Common.Abstractions.Messages.InterationEvents;

[ExcludeFromTopology]
public interface IEventMessage : IRequest, INotificationEvent { }
