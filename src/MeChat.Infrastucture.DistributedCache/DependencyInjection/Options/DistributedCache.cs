namespace MeChat.Infrastucture.DistributedCache.DependencyInjection.Options;
public sealed class DistributedCache
{
    public RedisCacheConfiguration RedisCacheConfiguration { get; set; } = new();
}
