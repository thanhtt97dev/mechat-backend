using MeChat.Application.UseCases.V1.Auth.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Application.DependencyInjection.Extentions;
public static class UtilSerivceExtention
{
    public static void AddApplicationUtils(this IServiceCollection services)
    {
        services.AddTransient<AuthUtil>();
    }
}
