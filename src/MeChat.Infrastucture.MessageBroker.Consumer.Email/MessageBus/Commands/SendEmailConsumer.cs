using MeChat.Common.MessageBroker.Email;
using MeChat.Infrastucture.MessageBroker.Consumer.Email.Abtractions.Messages;
using MediatR;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.MessageBus.Commands;

public class SendEmailConsumer : BaseConsumer<Command.SendEmail>
{
    public SendEmailConsumer(ISender sender) : base(sender)
    {
    }
}
