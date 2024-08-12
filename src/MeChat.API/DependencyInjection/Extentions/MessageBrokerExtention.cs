using MassTransit;
using MeChat.API.DependencyInjection.Options;

namespace MeChat.API.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    public static void AddMessageBrokerMasstransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

            configuration.UsingRabbitMq((context, busConfig) =>
            {
                busConfig.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, hostConfig =>
                {
                    hostConfig.Username(masstransitConfiguration.Username);
                    hostConfig.Password(masstransitConfiguration.Password);    
                });
                busConfig.ConfigureEndpoints(context);
            });
        });
    }
}
