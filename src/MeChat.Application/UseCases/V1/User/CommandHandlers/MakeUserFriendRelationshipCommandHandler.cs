using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.RealTime;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;
using MeChat.Domain.Entities;
using MeChat.Infrastucture.RealTime.Hubs;
using System.Text.Json;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class MakeUserFriendRelationshipCommandHandler : ICommandHandler<Command.MakeUserFriendRelationship>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepository<Domain.Entities.Friend> friendRepository;
    private readonly IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository;

    private readonly IRealTimeContext<ConnectionHub> notificationHubContext;

    public MakeUserFriendRelationshipCommandHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository,
        IRepository<Domain.Entities.Friend> friendRepository,
        IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository, 
        IRealTimeContext<ConnectionHub> notificationHubContext)
    {
        this.userRepository = userRepository;
        this.friendRepository = friendRepository;
        this.notificationRepository = notificationRepository;
        this.notificationHubContext = notificationHubContext;
    }

    public async Task<Result> Handle(Command.MakeUserFriendRelationship request, CancellationToken cancellationToken)
    {
        #region Validation
        if (request.UserId == request.FriendId)
            return Result.Failure("Invalid friend");

        if (!AppConstants.FriendStatusRequest.STATUS.Contains(request.Status))
            return Result.Failure("Invalid status");

        if(request.FriendId == null)
            return Result.Failure("Invalid friend");

        var user = await userRepository.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Failure("User not found");

        if (user.Status == AppConstants.User.Status.Deactivate)
            return Result.Initialization(AppConstants.ResponseCodes.User.Banned, "User has been banned!", false);
        #endregion

        var friendRelationship = await friendRepository.FindSingleAsync(x =>
                (x.UserFirstId == request.UserId && x.UserSecondId == request.FriendId) ||
                (x.UserSecondId == request.UserId && x.UserFirstId == request.FriendId));

        //unfriend -> add
        if (friendRelationship == null)
        {
            Friend friend = new Friend()
            {
                UserFirstId = request.UserId,
                UserSecondId = (Guid)request!.FriendId,
                SpecifierId = request.UserId,
                Status = AppConstants.FriendStatus.WatitingAccept,
            };

            await SendNotificationAsync(Guid.Parse(request.FriendId.ToString()!), request.UserId, AppConstants.FriendStatus.WatitingAccept);
            return Result.Success<object>(new { NewRelationshipStatus = AppConstants.FriendRealtionship.WatitingAccept });
        }

        //check is blocked by other
        if (friendRelationship.Status == AppConstants.FriendStatus.Block &&
            request.UserId != friendRelationship.SpecifierId)
            return Result.Initialization(AppConstants.ResponseCodes.User.FriendBlock, "Your frien blocked", false);

        int friendshipStatusUpdate = AppConstants.FriendStatus.UnFriend;
        int newFriendRelationship = AppConstants.FriendStatus.UnFriend;

        switch (request.Status)
        {
            case AppConstants.FriendStatusRequest.UnFriend:
                //watting -> unfriend => oke
                //accept -> unfriend => oke
                //block -> unfriend => need check blocker
                if (friendRelationship.Status == AppConstants.FriendStatus.Block && request.UserId != friendRelationship.SpecifierId)
                    return Result.Failure("Invalid request");

                friendshipStatusUpdate = AppConstants.FriendStatus.UnFriend;
                newFriendRelationship = AppConstants.FriendRealtionship.UnFriend;
                break;
            case AppConstants.FriendStatusRequest.WatitingAccept:
                //unfriend -> watting => oke
                //accept -> watting => invalid
                if (friendRelationship.Status == AppConstants.FriendStatus.Accepted) 
                    return Result.Failure("Invalid request");
                //block -> watting => check is blocker
                if (friendRelationship.Status == AppConstants.FriendStatus.Block && request.UserId != friendRelationship.SpecifierId)
                    return Result.Failure("Invalid request");

                //watting accept -> watting accept => accepted
                if (friendRelationship.Status == AppConstants.FriendStatus.WatitingAccept)
                {
                    friendshipStatusUpdate = AppConstants.FriendStatus.Accepted;
                    newFriendRelationship = AppConstants.FriendRealtionship.Accepted;

                    //send noti accepted
                    await SendNotificationAsync(Guid.Parse(request.FriendId.ToString()!), request.UserId, AppConstants.FriendStatus.Accepted);
                    break;
                }

                //send noti requset add friend
                await SendNotificationAsync(Guid.Parse(request.FriendId.ToString()!), request.UserId, AppConstants.FriendStatus.WatitingAccept);

                friendshipStatusUpdate = AppConstants.FriendStatus.WatitingAccept;
                newFriendRelationship = AppConstants.FriendRealtionship.WatitingAccept;
                break;
            case AppConstants.FriendStatusRequest.Accepted:
                //unfriend -> accept => invalid
                if (friendRelationship.Status == AppConstants.FriendStatus.UnFriend)
                    return Result.Failure("Invalid request");

                //watting -> accept => oke
                friendshipStatusUpdate = AppConstants.FriendStatus.Accepted;
                newFriendRelationship = AppConstants.FriendRealtionship.Accepted;

                //send noti accepted
                await SendNotificationAsync(Guid.Parse(request.FriendId.ToString()!), request.UserId, AppConstants.FriendStatus.Accepted);
                break;
            case AppConstants.FriendStatusRequest.Block:
                friendshipStatusUpdate = AppConstants.FriendStatus.Block;
                newFriendRelationship = AppConstants.FriendRealtionship.BlockRequester;
                break;
            case AppConstants.FriendStatusRequest.RequestUnBlock:
                friendshipStatusUpdate = friendRelationship.OldStatus;
                newFriendRelationship = friendRelationship.OldStatus;
                break;
        }

        //update friend relationship
        friendRelationship.SpecifierId = request.UserId;
        friendRelationship.OldStatus = friendRelationship.Status;
        friendRelationship.Status = friendshipStatusUpdate;
        friendRepository.Update(friendRelationship);

        return Result.Success<object>(new { NewRelationshipStatus = newFriendRelationship });
    }

    public async Task SendNotificationAsync(Guid userId, Guid friednId, int type)
    {
        var user = await userRepository.FindByIdAsync(userId);
        var friend = await userRepository.FindByIdAsync(friednId);

        var content = string.Empty;
        
        if (type == AppConstants.FriendStatus.WatitingAccept)
        {
            content = $"{friend.Fullname} đã gửi cho bạn yêu cầu kết bạn.";
        }
        else
        {
            content = $"{friend.Fullname} đã chấp nhận yêu cầu kết bạn.";
        }
        MeChat.Domain.Entities.Notification notification = new()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CreatedDate = DateTime.Now,
            Content = content,
            Image = friend.Avatar!,
            Link = $"{AppConstants.FrontEndEndpoints.Profile}/{friend.Id}",
            IsReaded = false
        };

        notificationRepository.Add(notification);

        var message = JsonSerializer.Serialize(notification);
        await notificationHubContext.SendMessageAsync(AppConstants.RealTime.Method.Notification, userId, message);
    }

}
