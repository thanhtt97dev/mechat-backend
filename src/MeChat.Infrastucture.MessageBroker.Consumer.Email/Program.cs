
using MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;
using MeChat.Infrastucture.Service.DependencyInjection.Extentions;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Add MediatR
        builder.Services.AddConfigurationMediatR();

        //Add messagebroker
        builder.Services.AddMessageBrokerMasstransitRabbitMq(builder.Configuration);

        //Add email service
        builder.Services.AddEmailConfiguration();

        var app = builder.Build();

        app.Run();
    }
}
