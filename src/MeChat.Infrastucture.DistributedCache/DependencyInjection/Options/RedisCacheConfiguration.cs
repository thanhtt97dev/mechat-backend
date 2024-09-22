namespace MeChat.Infrastucture.DistributedCache.DependencyInjection.Options;
public sealed class RedisCacheConfiguration
{
    public string ConnectionStrings { get; set; } = string.Empty;
}
