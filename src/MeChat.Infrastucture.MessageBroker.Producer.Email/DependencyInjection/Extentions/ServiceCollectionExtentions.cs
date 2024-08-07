using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.MessageBroker.Producer.Email.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Producer.Email.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddMessageBrokerProducerEmail(this IServiceCollection services)
    {
        services.AddTransient<IMessageBrokerProducerEmail, MessageBrokerProducerEmail>();
    }
}
