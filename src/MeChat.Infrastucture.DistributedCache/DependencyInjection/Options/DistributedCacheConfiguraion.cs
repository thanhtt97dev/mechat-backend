namespace MeChat.Infrastucture.DistributedCache.DependencyInjection.Options;
public sealed class DistributedCacheConfiguraion
{
    public string Mode { get; set; } = string.Empty;
    public string ConnectionStrings { get; set;} = string.Empty;
}
