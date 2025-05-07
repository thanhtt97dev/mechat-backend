using MeChat.Common.Abstractions.RealTime;
using MeChat.Common.Abstractions.Services;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace MeChat.Infrastructure.RealTime.Services;
public class RealTimeConnectionManager : IRealTimeConnectionManager
{
    private readonly ICacheService cacheService;

    public RealTimeConnectionManager(ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    public async Task AddConnection(string key, string endpoint, string connectionId)
    {
        var keyStoreData = GetKeyStoreData(key, endpoint);
        string? data = await cacheService.GetCache(keyStoreData);
        HashSet<string> connectionIds = new HashSet<string>();
        if (data == null)
        {
            connectionIds.Add(connectionId);
            await cacheService.SetCache(keyStoreData, connectionIds);
            return;
        }

        connectionIds = JsonSerializer.Deserialize<HashSet<string>>(data)!;
        connectionIds.Add(connectionId);

        await cacheService.SetCache(keyStoreData, connectionIds);
    }

    public async Task<HashSet<string>> GetConnections(string key, string endpoint)
    {
        var keyStoreData = GetKeyStoreData(key, endpoint);
        string? data = await cacheService.GetCache(keyStoreData);
        if (data == null) 
            return new HashSet<string>();

        HashSet<string> connectionIds = JsonSerializer.Deserialize<HashSet<string>>(data)!;
        return connectionIds;
    }

    public async Task<bool> IsConnected(string key, string endpoint)
    {
        var keyStoreData = GetKeyStoreData(key, endpoint);
        string? data = await cacheService.GetCache(keyStoreData);
        if (data == null) return false;
        return true;
    }

    public async Task RemoveConnection(string key, [Optional] string endpoint, [Optional] string connectionId)
    {
        var keyStoreData = GetKeyStoreData(key, endpoint);
        string? data = await cacheService.GetCache(keyStoreData);
        if (data == null) return;

        HashSet<string> connectionIds = JsonSerializer.Deserialize<HashSet<string>>(data)!;
        if (connectionId == default(string))
            await cacheService.RemoveCache(keyStoreData);

        connectionIds.Remove(connectionId!);
        if(connectionIds.Count == 0)
        {
            await cacheService.RemoveCache(keyStoreData);
            return;
        }
        await cacheService.SetCache(keyStoreData, connectionIds);
    }

    private string GetKeyStoreData(string key, string endpoint) 
    {
        return $"{endpoint}-{key}";
    }

   
}
