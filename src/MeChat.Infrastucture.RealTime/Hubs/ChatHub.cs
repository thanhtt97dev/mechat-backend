using Microsoft.AspNetCore.SignalR;

namespace MeChat.Infrastucture.RealTime.Hubs;
public class ChatHub : Hub
{
    public ChatHub()
    {
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("hieuld02-connect");
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Clients.All.SendAsync("hieuld02-disconnect");
    }

}
