namespace MeChat.Infrastucture.MessageBroker.Producer.Email.DependencyInjection.Options;
public sealed class MessageBrokerConfiguration
{
    public RabbitMqConfiguration RabbitMqConfiguration { get; set; } = new();
}
