﻿using MassTransit;
using MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Options;
using System.Reflection;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    public static void AddMessageBrokerAzureServiceBus(IServiceCollection services, IConfiguration configuration)
    {

    }

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
