using System.Runtime.InteropServices;

namespace MeChat.Common.Abstractions.Services;
public interface IRealTimeConnectionManager<T>
{
    void AddConnection(T key, string endpoint, string connectionId);
    void RemoveConnection(T key, [Optional]string endpoint, [Optional]string connectionId);
    void GetConnections(T key, string endpoint);
    bool IsUserConnected(T key, [Optional] string endpoint);
}
