using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.DistributedCache.DependencyInjection.Extentions;
public static class ServiceCollectionExtention
{
    public static void AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedCacheRedis(configuration);
    }
}
