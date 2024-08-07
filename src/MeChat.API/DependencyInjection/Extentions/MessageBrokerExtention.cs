using MassTransit;
using MeChat.API.DependencyInjection.Options;

namespace MeChat.API.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    public static void AddMessageBrokerMasstransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransitOption = new MasstransitOption();
        configuration.GetSection(nameof(MasstransitOption)).Bind(masstransitOption);

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

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
