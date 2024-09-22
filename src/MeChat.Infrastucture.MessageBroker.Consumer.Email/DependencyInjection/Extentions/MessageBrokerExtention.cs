using MassTransit;
using MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Options;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    #region AddMessageBrokerAzureServiceBus
    public static void AddMessageBrokerAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new MessageBrokerConfiguration();
        configuration.GetSection(nameof(MessageBrokerConfiguration)).Bind(messageBrokerConfig);

        AzureServiceBusConfiguration azureServiceBusConfig = messageBrokerConfig.AzureServiceBusConfiguration;

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


    public static void AddMessageBrokerMasstransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new MessageBrokerConfiguration();
        configuration.GetSection(nameof(MessageBrokerConfiguration)).Bind(messageBrokerConfig);

        RabbitMqConfiguration rabbitMqConfiguration = messageBrokerConfig.RabbitMqConfiguration;

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
}
