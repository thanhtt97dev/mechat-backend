namespace MeChat.Common.Shared.Configurations;
public sealed class DistributedCache
{
    public string Mode { get; set; } = nameof(DistributedCache.InMemory);
    public InMemoryDistributedCache InMemory { get; set; } = new();
    public Redis Redis { get; set; } = new();
}

public sealed class InMemoryDistributedCache
{
    
}

public sealed class Redis
{
    public string ConnectionStrings { get; set; } = string.Empty;
}