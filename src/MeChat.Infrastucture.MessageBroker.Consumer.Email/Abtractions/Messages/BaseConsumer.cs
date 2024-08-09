using MassTransit;
using MeChat.Common.Abstractions.Messages.InterationEvents;
using MediatR;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.Abtractions.Messages;

public abstract class BaseConsumer<TMessage> : IConsumer<TMessage>
    where TMessage : class, INotificationEvent
{
    private readonly ISender sender;

    protected BaseConsumer(ISender sender)
    {
        this.sender = sender;
    }

    public Task Consume(ConsumeContext<TMessage> context)
        => sender.Send(context.Message);
}
