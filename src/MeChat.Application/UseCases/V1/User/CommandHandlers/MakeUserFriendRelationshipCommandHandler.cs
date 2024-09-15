using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;
using MeChat.Domain.Entities;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class MakeUserFriendRelationshipCommandHandler : ICommandHandler<Command.MakeUserFriendRelationship>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepository<Domain.Entities.Friend> friendRepository;

    public MakeUserFriendRelationshipCommandHandler(
            IRepositoryBase<Domain.Entities.User,
            Guid> userRepository, IRepository<Friend> friendRepository)
    {
        this.userRepository = userRepository;
        this.friendRepository = friendRepository;
    }

    public async Task<Result> Handle(Command.MakeUserFriendRelationship request, CancellationToken cancellationToken)
    {
        if (request.UserId == request.FriendId)
            return Result.Failure("Invalid friend");

        if (!AppConstants.FriendStatus.STATUS.Contains(request.Status))
            return Result.Failure("Invalid status");

        var user = await userRepository.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Failure("User not found");

        if (user.Status == AppConstants.User.Status.Deactivate)
            return Result.Initialization(AppConstants.ResponseCodes.User.Banned, "User has been banned!", false);

        var friendRelationship = await friendRepository.FindSingleAsync(x =>
                (x.UserFirstId == request.UserId && x.UserSecondId == request.FriendId) ||
                (x.UserSecondId == request.UserId && x.UserFirstId == request.FriendId)
            );

        //unfriend -> add
        if (friendRelationship == null)
        {
            Friend friend = new Friend()
            {
                UserFirstId = request.UserId,
                UserSecondId = request.FriendId,
                SpecifierId = request.UserId,
                Status = AppConstants.FriendStatus.WatitingAccept,
            };

            return Result.Success();
        }

        //check is blocked by other
        if (friendRelationship.Status == AppConstants.FriendStatus.Block &&
            request.UserId != friendRelationship.SpecifierId)
            return Result.Initialization(AppConstants.ResponseCodes.User.FriendBlock, "Your frien blocked", false);

        if (request.Status == friendRelationship!.Status)
            return Result.Success();

        if (request.Status == AppConstants.FriendStatus.UnFriend)
        {
            //watting -> unfriend => oke
            //accept -> unfriend => oke
            //block -> unfriend => need check blocker
            if (friendRelationship.Status == AppConstants.FriendStatus.Block &&
                request.UserId != friendRelationship.SpecifierId)
                return Result.Failure("Invalid request");
        }
        else if (request.Status == AppConstants.FriendStatus.WatitingAccept)
        {
            //unfriend -> watting => oke
            //accept -> watting => invalid
            if (friendRelationship.Status == AppConstants.FriendStatus.Accepted)
                return Result.Failure("Invalid request");
            //block -> watting => check is blocker
            if (friendRelationship.Status == AppConstants.FriendStatus.Block &&
                request.UserId != friendRelationship.SpecifierId)
                return Result.Failure("Invalid request");
        }
        else if (request.Status == AppConstants.FriendStatus.Accepted)
        {
            //unfriend -> accept => invalid
            if (friendRelationship.Status == AppConstants.FriendStatus.UnFriend)
                return Result.Failure("Invalid request");

            //watting -> accept => oke

            //block -> accept => check is blocker
            if (friendRelationship.Status == AppConstants.FriendStatus.Block &&
                request.UserId != friendRelationship.SpecifierId)
                return Result.Failure("Invalid request");
        }
        //Can block in any time

        //update friend relationship
        friendRelationship.SpecifierId = request.UserId;
        friendRelationship.OldStatus = friendRelationship.Status;
        friendRelationship.Status = request.Status;
        friendRepository.Update(friendRelationship);

        return Result.Success();
    }
}
