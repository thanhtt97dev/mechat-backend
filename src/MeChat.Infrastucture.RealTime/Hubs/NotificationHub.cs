﻿using MassTransit.JobService;
using MeChat.Common.Abstractions.RealTime;
using MeChat.Common.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace MeChat.Infrastucture.RealTime.Hubs;

[Authorize]
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
