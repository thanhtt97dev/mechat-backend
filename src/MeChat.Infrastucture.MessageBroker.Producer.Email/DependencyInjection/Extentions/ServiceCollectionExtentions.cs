using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.MessageBroker.Producer.Email.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Producer.Email.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBrokerMasstransitRabbitMq(configuration);
        //services.AddMessageBrokerAzureServiceBus(configuration);
    }

    public static void AddMessageBrokerProducerEmail(this IServiceCollection services)
    {
        services.AddTransient<IMessageBrokerProducerEmail, MessageBrokerProducerEmail>();
    }
}
