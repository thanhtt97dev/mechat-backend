using MeChat.Common.Abstractions.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.InteropServices;

namespace MeChat.Infrastucture.DistributedCache.Services;
public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<string?> GetCache(string key)
    {
        _memoryCache.TryGetValue(key, out string? value);
        return Task.FromResult(value);
    }

    public Task RemoveCache(string key)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetCache(string key, object value, [Optional] TimeSpan timeOut)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeOut == TimeSpan.Zero ? TimeSpan.FromMinutes(30) : timeOut
        };

        _memoryCache.Set(key, value, cacheEntryOptions);
        return Task.CompletedTask;
    }
}
