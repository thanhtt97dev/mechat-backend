using System.Runtime.InteropServices;

namespace MeChat.Common.Abstractions.Services;
public interface ICacheService
{
    Task<string?> GetCache(string key);
    Task RemoveCache(string key);
    Task SetCache(string key, object value, [Optional]TimeSpan timeOut);
}
