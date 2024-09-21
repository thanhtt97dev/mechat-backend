using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Abstractions.RealTime;
using MeChat.Infrastucture.RealTime.Services;

namespace MeChat.Infrastucture.RealTime.DependencyInjection.Extentions;

public static class RealTimeExtention
{
    public static void AddConfigRealTime(this IServiceCollection services)
    {
        services.AddSignalR(c =>
        {
            c.EnableDetailedErrors = true;
            c.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            c.KeepAliveInterval = TimeSpan.FromSeconds(15);
        });

        services.AddTransient<IRealTimeConnectionManager, RealTimeConnectionManager>();
        services.AddTransient(typeof(IRealTimeContext<>), typeof(RealTimeSignalRContext<>));
    }

    public static IApplicationBuilder MapRealTimeEndpoints(this IApplicationBuilder builder)
    {
        return builder;
    }

}
