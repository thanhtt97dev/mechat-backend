using MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;
using MeChat.Infrastucture.Service.DependencyInjection.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                //Add MediatR
                services.AddConfigurationMediatR();

                //Add messagebroker
                services.AddMessageBroker(configuration);

                //Add email service
                services.AddEmailConfiguration();
            })
            .Build();

        // Start the host
        await host.RunAsync();
    }
}
