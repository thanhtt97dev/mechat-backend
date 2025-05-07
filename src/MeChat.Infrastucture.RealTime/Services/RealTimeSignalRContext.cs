using MeChat.Common.Abstractions.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace MeChat.Infrastructure.RealTime.Services;
public class RealTimeSignalRContext<T> : IRealTimeContext<T>
    where T : Hub
{
    private readonly IHubContext<T> hubContext;
    private readonly IRealTimeConnectionManager connectionManager;

    public RealTimeSignalRContext(
        IHubContext<T> hubContext, 
        IRealTimeConnectionManager connectionManager)
    {
        this.hubContext = hubContext;
        this.connectionManager = connectionManager;
    }

    public async Task SendMessageAsync(string method, IReadOnlyList<string> connectionIds, string message)
    {
        if(string.IsNullOrEmpty(method) || connectionIds == null || string.IsNullOrEmpty(message))
            throw new Exception("Invalid to send message - realtime");

        await hubContext.Clients.Clients(connectionIds).SendAsync(method, message);
    }

    public async Task SendMessageAsync(string method, Guid userId, string message)
    {
        var connectionIds = await connectionManager.GetConnections(userId.ToString(), method);
        if (connectionIds.Count == 0)
            return;

        await hubContext.Clients.Clients(connectionIds).SendAsync(method, message);
    }
}
