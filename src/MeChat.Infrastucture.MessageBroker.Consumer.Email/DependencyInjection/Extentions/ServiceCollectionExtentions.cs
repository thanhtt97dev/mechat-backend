namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class ServiceCollectionExtentions
{
    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBrokerMasstransitRabbitMq(configuration);
        //services.AddMessageBrokerAzureServiceBus(configuration);
    }
}
