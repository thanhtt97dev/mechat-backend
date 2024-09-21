using MeChat.Common.Abstractions.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.InteropServices;

namespace MeChat.Infrastucture.DistributedCache.Extentions;
public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache distributedCache;
    //private readonly IConnectionMultiplexer connectionMultiplexer;

    public RedisCacheService(IDistributedCache distributedCache)
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

    public async Task SetCache(string key, object value, [Optional] TimeSpan timeOut)
    {
        if (value == null)
            return;

        var serialierValue = JsonConvert.SerializeObject(value, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        if (timeOut == default(TimeSpan))
        {
            await distributedCache.SetStringAsync(key, serialierValue);
            return;
        }

        await distributedCache.SetStringAsync(key, serialierValue,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
    }
}
