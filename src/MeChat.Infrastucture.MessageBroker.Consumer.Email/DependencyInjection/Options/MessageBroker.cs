namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Options;

public sealed class MessageBroker
{
    public RabbitMq RabbitMqConfiguration { get; set; } = new();
    public AzureServiceBus AzureServiceBusConfiguration { get; set; } = new();
}