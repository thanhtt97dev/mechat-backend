using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Mail.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Mail.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddMailService(this IServiceCollection services)
    {
        services.AddTransient<IMailService, MailService>();
    }
}
