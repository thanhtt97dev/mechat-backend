using MeChat.Common.Abstractions.RealTime;
using MeChat.Common.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MeChat.Infrastructure.RealTime.Hubs;

[Authorize]
public class ConnectionHub : Hub
{
    private readonly IRealTimeConnectionManager connectionManager;

    public ConnectionHub(IRealTimeConnectionManager connectionManager)
    {
        this.connectionManager = connectionManager;
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var userId = Context.User!.Claims.FirstOrDefault(x => x.Type == AppConstants.Configuration.Jwt.id)!.Value;
        await connectionManager.AddConnection(userId, AppConstants.RealTime.Method.Notification, connectionId);
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var userId = Context.User!.Claims.FirstOrDefault(x => x.Type == AppConstants.Configuration.Jwt.id)!.Value;
        await connectionManager.RemoveConnection(userId, AppConstants.RealTime.Method.Notification, connectionId);
        await base.OnDisconnectedAsync(exception);
    }

}
