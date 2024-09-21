using System.Runtime.InteropServices;

namespace MeChat.Common.Abstractions.RealTime;
public interface IRealTimeConnectionManager
{
    Task AddConnection(string key, string endpoint, string connectionId);
    Task RemoveConnection(string key, [Optional] string endpoint, [Optional] string connectionId);
    Task<HashSet<string>> GetConnections(string key, string endpoint);
    Task<bool> IsConnected(string key, string endpoint);
}
