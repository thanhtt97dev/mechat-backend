using MassTransit;
using MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Options;
using MeChat.Infrastucture.MessageBroker.Consumer.Email.MessageBus.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    #region Add Message Broker
    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        //services.RabbitMq(configuration);
        services.AzureServiceBus(configuration);
    }
    #endregion

    #region AzureServiceBus
    public static void AzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Options.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        AzureServiceBus azureServiceBusConfig = messageBrokerConfig.AzureServiceBusConfiguration;

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();
            configuration.AddConsumers(AssemblyReference.Assembly);
            configuration.UsingAzureServiceBus((context, config) =>
            {
                config.Host(azureServiceBusConfig.ConnectionString);
                config.ConfigureEndpoints(context);
            });
        });
    }
    #endregion

    #region RabbitMq
    public static void RabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Options.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        RabbitMq rabbitMqConfiguration = messageBrokerConfig.RabbitMqConfiguration;

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

            configuration.AddConsumers(AssemblyReference.Assembly);

            configuration.UsingRabbitMq((context, busConfig) =>
            {
                busConfig.Host(rabbitMqConfiguration.Host, rabbitMqConfiguration.VHost, hostConfig =>
                {
                    hostConfig.Username(rabbitMqConfiguration.Username);
                    hostConfig.Password(rabbitMqConfiguration.Password);
                });
                busConfig.ConfigureEndpoints(context);
            });
        });
    }
    #endregion

}
