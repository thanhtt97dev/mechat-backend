namespace MeChat.Common.Shared.Configurations;
public sealed class MessageBroker
{
    public string Mode { get; set; } = nameof(MessageBroker.RabbitMq);
    public InMemoryMessageBroker InMemory { get; set; } = new();
    public RabbitMq RabbitMq { get; set; } = new();
    public AzureServiceBus AzureServiceBus { get; set; } = new();
}

public sealed class InMemoryMessageBroker 
{

}

public sealed class RabbitMq
{
    public string Host { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string VHost { get; set; } = string.Empty;
}

public sealed class AzureServiceBus
{
    public string ConnectionString { get; set; } = string.Empty;
}