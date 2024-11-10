using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.DistributedCache.Extentions;
using MeChat.Infrastucture.DistributedCache.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MeChat.Infrastucture.DistributedCache.DependencyInjection.Extentions;
public static class CacheExtention
{
    #region Add Distributed Cache
    public static void AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        Common.Shared.Configurations.DistributedCache distributedCacheConfig = new();
        configuration.GetSection(nameof(DistributedCache)).Bind(distributedCacheConfig);

        switch (distributedCacheConfig.Mode)
        {
            case nameof(Common.Shared.Configurations.DistributedCache.InMemory):
                services.AddInMemoryCache(configuration);
                break;
            case nameof(Common.Shared.Configurations.DistributedCache.Redis):
                services.AddRedis(configuration);
                break;
            default:
                services.AddInMemoryCache(configuration);
                break;
        }
    }
    #endregion

    #region InMemory
    private static void AddInMemoryCache(this IServiceCollection services, IConfiguration configuration)
    {
        //configuration
        services.AddMemoryCache();

        //DI
        services.AddSingleton<ICacheService, InMemoryCacheService>();
    }
    #endregion

    #region Redis
    private static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        //configuration
        Common.Shared.Configurations.DistributedCache distributedCacheConfiguration = new();
        configuration.GetSection(nameof(DistributedCache)).Bind(distributedCacheConfiguration);
        var connectionStrings = distributedCacheConfiguration.Redis.ConnectionStrings;

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionStrings));
        services.AddStackExchangeRedisCache(options => options.Configuration = connectionStrings);

        //DI
        services.AddSingleton<ICacheService, RedisCacheService>();
    }
    #endregion

}
