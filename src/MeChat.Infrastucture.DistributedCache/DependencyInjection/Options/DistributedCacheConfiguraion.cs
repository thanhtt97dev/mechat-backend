namespace MeChat.Infrastucture.DistributedCache.DependencyInjection.Options;
public sealed class DistributedCacheConfiguraion
{
    public RedisCacheConfiguration RedisCacheConfiguration { get; set; } = new();
}
