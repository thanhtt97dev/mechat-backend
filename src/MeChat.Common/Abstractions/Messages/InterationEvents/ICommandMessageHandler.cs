using MediatR;

namespace MeChat.Common.Abstractions.Messages.InterationEvents;
public interface ICommandMessageHandler<TMessage> : IRequestHandler<TMessage> 
    where TMessage : ICommandMessage
{ }
