namespace MeChat.Common.Abstractions.Services;
public interface ICacheService
{
    Task<string?> GetCache(string key);
    Task SetCache(string key, object value, TimeSpan timeOut);
}
