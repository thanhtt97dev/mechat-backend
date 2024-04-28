using MeChat.Common.Abstractions.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace MeChat.Infrastucture.Redis.Extentions;
public class CacheService : ICacheService
{
    private readonly IDistributedCache distributedCache;
    //private readonly IConnectionMultiplexer connectionMultiplexer;

    public CacheService(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    public async Task<string?> GetCache(string key)
    {
        var result = await distributedCache.GetStringAsync(key);
        if (string.IsNullOrEmpty(result))
            return null;
        return result;
    }

    public async Task RemoveCache(string key)
    {
        await distributedCache.RemoveAsync(key);
    }

    public async Task SetCache(string key, object value, TimeSpan timeOut)
    {
        if (value == null)
            return;

        var serialierValue = JsonConvert.SerializeObject(value, new JsonSerializerSettings
        {
            ContractResolver = new  CamelCasePropertyNamesContractResolver()
        });

        var distributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeOut
        };

        await distributedCache.SetStringAsync(key, serialierValue, distributedCacheEntryOptions);
    }
}
