using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.DistributedCache.DependencyInjection.Options;
using MeChat.Infrastucture.DistributedCache.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MeChat.Infrastucture.DistributedCache.DependencyInjection.Extentions;
public static class CacheExtention
{
    public static void AddDistributedCacheRedis(this IServiceCollection services, IConfiguration configuration)
    {
        Options.DistributedCache distributedCacheConfiguration = new();
        configuration.GetSection(nameof(DistributedCache)).Bind(distributedCacheConfiguration);
        var connectionStrings = distributedCacheConfiguration.RedisCacheConfiguration.ConnectionStrings;

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionStrings));
        services.AddStackExchangeRedisCache(options => options.Configuration = connectionStrings);
        services.AddSingleton<ICacheService, RedisCacheService>();
    }
}
