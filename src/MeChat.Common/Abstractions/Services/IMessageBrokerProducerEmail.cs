namespace MeChat.Common.Abstractions.Services;
public interface IMessageBrokerProducerEmail
{
    Task SendMailAsync(string email, string subject, string content);
}
