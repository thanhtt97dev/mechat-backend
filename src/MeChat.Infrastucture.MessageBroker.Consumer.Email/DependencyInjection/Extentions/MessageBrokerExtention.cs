using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    #region Add Message Broker
    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        Common.Shared.Configurations.MessageBroker messageBrokerConfig = new();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        switch (messageBrokerConfig.Mode)
        {
            //case nameof(Common.Shared.Configurations.MessageBroker.InMemory):
            //    services.AddInMemory(configuration);
            //    break;
            case nameof(Common.Shared.Configurations.MessageBroker.RabbitMq):
                services.AddRabbitMq(configuration);
                break;
            case nameof(Common.Shared.Configurations.MessageBroker.AzureServiceBus):
                services.AzureServiceBus(configuration);
                break;
            default:
                services.AddRabbitMq(configuration);
                break;
        }
    }
    #endregion

    #region InMemory
    private static void AddInMemory(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();
            configuration.AddConsumers(AssemblyReference.Assembly);
            configuration.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
    }
    #endregion

    #region RabbitMq
    private static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Common.Shared.Configurations.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        Common.Shared.Configurations.RabbitMq rabbitMqConfiguration = messageBrokerConfig.RabbitMq;

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

    #region AzureServiceBus
    private static void AzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Common.Shared.Configurations.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        Common.Shared.Configurations.AzureServiceBus azureServiceBusConfig = messageBrokerConfig.AzureServiceBus;

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

}
