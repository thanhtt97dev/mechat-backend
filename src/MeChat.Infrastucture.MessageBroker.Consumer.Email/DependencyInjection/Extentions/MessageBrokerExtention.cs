using MassTransit;
using MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Options;
using System.Reflection;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    public static void AddMessageBrokerMasstransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransitOption = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitOption);

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

            configuration.AddConsumers(AssemblyReference.Assembly);

            configuration.UsingRabbitMq((context, busConfig) =>
            {
                busConfig.Host(masstransitOption.Host, masstransitOption.VHost, hostConfig =>
                {
                    hostConfig.Username(masstransitOption.Username);
                    hostConfig.Password(masstransitOption.Password);
                });
                busConfig.ConfigureEndpoints(context);
            });
        });
    }
}
