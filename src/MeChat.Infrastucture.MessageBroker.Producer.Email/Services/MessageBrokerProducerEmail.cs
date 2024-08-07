using MassTransit;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.MessageBroker.Email;

namespace MeChat.Infrastucture.MessageBroker.Producer.Email.Services;
public class MessageBrokerProducerEmail : IMessageBrokerProducerEmail
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageBrokerProducerEmail(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task SendMailAsync(string email, string subject, string content)
    {
        var message = new Command.SendEmail
        {
            Id = Guid.NewGuid(),
            Name = "send email",
            Description = subject,
            TimeStamp = DateTime.UtcNow,
            Type = "daw"
        };
        await _publishEndpoint.Publish(message);
    }
}
