using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Redis.DependencyInjection.Options;
using MeChat.Infrastucture.Redis.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MeChat.Infrastucture.Redis.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddDistributedCacheRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration["DistributedCacheConfiguraion:ConnectionStrings"] ?? string.Empty;

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionStrings));
        services.AddStackExchangeRedisCache(options => options.Configuration = connectionStrings);
        services.AddSingleton<ICacheService, RedisCacheService>();
    }
}
