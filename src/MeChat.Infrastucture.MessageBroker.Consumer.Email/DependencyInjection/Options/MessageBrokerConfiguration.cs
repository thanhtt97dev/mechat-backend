namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Options;

public sealed class MessageBrokerConfiguration
{
    public RabbitMqConfiguration RabbitMqConfiguration { get; set; } = new();
}