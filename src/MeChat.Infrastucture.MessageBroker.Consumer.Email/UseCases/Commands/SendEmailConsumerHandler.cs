using MeChat.Common.Abstractions.Messages.InterationEvents;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.MessageBroker.Email;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.UseCases.Commands;

public class SendEmailConsumerHandler : ICommandMessageHandler<Command.SendEmail>
{
    private readonly IEmailService emailService;

    public SendEmailConsumerHandler(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public async Task Handle(Command.SendEmail request, CancellationToken cancellationToken)
    {
        await emailService.SendMailAsync(request.Emails, request.Subject, request.Content);
    }
}
