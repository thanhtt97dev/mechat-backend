using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Service.DependencyInjection.Extentions;
public static class EmailExtention
{
    public static void AddEmailConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
    }
}
