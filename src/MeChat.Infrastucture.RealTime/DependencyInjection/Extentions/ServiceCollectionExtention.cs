using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.RealTime.DependencyInjection.Extentions;
public static class ServiceCollectionExtention
{
    public static void AddRealTime(this IServiceCollection services)
    {
        services.AddConfigSignalR();
    }
}
