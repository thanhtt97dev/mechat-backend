using MediatR;

namespace MeChat.Common.Abstractions.Messages.InterationEvents;
public interface IEventMessageHandler <TMessage> : IRequestHandler<TMessage>
    where TMessage : IEventMessage
{ }
