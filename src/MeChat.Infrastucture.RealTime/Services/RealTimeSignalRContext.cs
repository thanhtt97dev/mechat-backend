using MeChat.Common.Abstractions.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace MeChat.Infrastucture.RealTime.Services;
public class RealTimeSignalRContext<T> : IRealTimeContext<T>
    where T : Hub
{
    private readonly IHubContext<T> hubContext;

    public RealTimeSignalRContext(IHubContext<T> hubContext)
    {
        this.hubContext = hubContext;
    }

    public async Task SendMessageAsync(string method, IReadOnlyList<string> connectionIds, string message)
    {
        if(string.IsNullOrEmpty(method) || connectionIds == null || string.IsNullOrEmpty(message))
            throw new Exception("Invalid to send message - realtime");

        await hubContext.Clients.Clients(connectionIds).SendAsync(method, message);
    }
}
