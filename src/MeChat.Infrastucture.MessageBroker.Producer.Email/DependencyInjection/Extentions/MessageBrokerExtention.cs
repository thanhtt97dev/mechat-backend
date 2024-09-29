using Azure.Messaging.ServiceBus.Administration;
using MassTransit;
using MeChat.Common.MessageBroker.Email;
using MeChat.Infrastucture.MessageBroker.Producer.Email.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Producer.Email.DependencyInjection.Extentions;
public static class MessageBrokerExtention
{
    #region AzureServiceBus
    public static void AzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Options.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        AzureServiceBus azureServiceBusConfig = messageBrokerConfig.AzureServiceBus;

        AddAzureServiceBusQueues(azureServiceBusConfig);

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

            configuration.UsingAzureServiceBus((context, config) =>
            {
                config.Host(azureServiceBusConfig.ConnectionString);
                config.ConfigureEndpoints(context);
            });
        });
    }

    private static void AddAzureServiceBusQueues(AzureServiceBus azureServiceBusConfig)
    {
        var azureAdmin = new ServiceBusAdministrationClient(azureServiceBusConfig.ConnectionString);

        List<string> queues = new()
        {
            KebabCaseEndpointNameFormatter.Instance.SanitizeName(nameof(Command.SendEmail)),
        };

        foreach (var queue in queues)
        {
            if (azureAdmin.QueueExistsAsync(queue).Result)
                continue;
            azureAdmin.CreateQueueAsync(new CreateQueueOptions(queue)
            {
                MaxSizeInMegabytes = 1024,
                DefaultMessageTimeToLive = TimeSpan.FromDays(1),
                RequiresDuplicateDetection = false,
                MaxDeliveryCount = 3,
                RequiresSession = false
            });
        }

        
    }

    #endregion

    #region RabbitMq
    public static void RabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Options.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        RabbitMq rabbitMqConfiguration = messageBrokerConfig.RabbitMq;

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

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
