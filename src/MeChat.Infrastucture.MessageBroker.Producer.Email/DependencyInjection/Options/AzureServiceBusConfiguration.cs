namespace MeChat.Infrastucture.MessageBroker.Producer.Email.DependencyInjection.Options;
public sealed class AzureServiceBusConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;
}
