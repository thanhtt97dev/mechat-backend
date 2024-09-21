using MeChat.Common.Abstractions.RealTime;
using MeChat.Common.Shared.Constants;
using Microsoft.AspNetCore.SignalR;

namespace MeChat.Infrastucture.RealTime.Hubs;
public class ChatHub : Hub
{
    private readonly IRealTimeConnectionManager connectionManager;

    public ChatHub(IRealTimeConnectionManager connectionManager)
    {
        this.connectionManager = connectionManager;
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        await connectionManager.AddConnection("hieuld", AppConstants.Configuration.RealTime.Chat, connectionId);
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Clients.All.SendAsync("hieuld02-disconnect");
    }

}
