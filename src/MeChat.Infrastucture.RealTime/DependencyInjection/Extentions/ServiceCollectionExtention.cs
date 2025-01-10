using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MeChat.Infrastructure.RealTime.Hubs;
using MeChat.Common.Shared.Constants;

namespace MeChat.Infrastructure.RealTime.DependencyInjection.Extentions;
public static class ServiceCollectionExtention
{
    public static void AddRealTime(this IServiceCollection services)
    {
        services.AddConfigSignalR();
    }

    public static void MapRealTimeEndpoints(this WebApplication app)
    {
        app.MapHub<ConnectionHub>(AppConstants.RealTime.Endpoint.Connection);
    }
}
