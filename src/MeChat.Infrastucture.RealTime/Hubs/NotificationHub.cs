using MeChat.Common.Abstractions.RealTime;
using MeChat.Common.Shared.Constants;
using Microsoft.AspNetCore.SignalR;

namespace MeChat.Infrastucture.RealTime.Hubs;
public class NotificationHub : Hub
{
    private readonly IRealTimeConnectionManager connectionManager;

    public NotificationHub(IRealTimeConnectionManager connectionManager)
    {
        this.connectionManager = connectionManager;
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        await connectionManager.AddConnection("hieuld", AppConstants.RealTime.Method.Notification, connectionId);
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Clients.All.SendAsync("hieuld02-disconnect");
    }

}
