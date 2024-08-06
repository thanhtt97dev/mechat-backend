using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Service.Email.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Service.Email.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddEmailService(this IServiceCollection services)
    {
        services.AddTransient<IMailService, MailService>();
    }
}
