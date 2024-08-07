using MeChat.Common.Abstractions.Messages.InterationEvents;
using MeChat.Common.MessageBroker.Email;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.UseCases.Commands;

public class SendEmailConsumerHandler : ICommandMessageHandler<Command.SendEmail>
{
    public Task Handle(Command.SendEmail request, CancellationToken cancellationToken)
    {
        Console.Write("hieudw");
        return Task.CompletedTask;
    }
}
