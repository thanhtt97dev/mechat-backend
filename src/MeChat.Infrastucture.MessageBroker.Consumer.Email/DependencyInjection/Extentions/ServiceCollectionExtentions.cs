using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class ServiceCollectionExtentions
{
    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        //services.RabbitMq(configuration);
        services.AzureServiceBus(configuration);
    }
}
