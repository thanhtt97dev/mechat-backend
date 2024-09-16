using AutoMapper;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.QueryHandlers;
public class GetUserPublicInfoQueryHandler : IQueryHandler<Query.GetUserPublicInfo, Response.UserPublicInfo>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepository<Domain.Entities.Friend> friendRepository;
    private readonly IMapper mapper;

    public GetUserPublicInfoQueryHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository,
        IRepository<Domain.Entities.Friend> friendRepository,
        IMapper mapper)
    {
        this.userRepository = userRepository;
        this.friendRepository = friendRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Response.UserPublicInfo>> Handle(Query.GetUserPublicInfo request, CancellationToken cancellationToken)
    {
        Guid userId;
        Guid.TryParse(request.Key, out userId);

        var user = await userRepository
                .FindSingleAsync(x => x.Id == userId || x.Username == request.Key || x.Email == request.Key);
        if (user == null)
            return Result.NotFound<Response.UserPublicInfo>("User not found!");

        var result = mapper.Map<Response.UserPublicInfo>(user);

        //get friends info
        int totalFriend = 0;
        List<Response.UserPublicInfo.FriendInfo> top5FriendInfo = new();

        totalFriend = friendRepository.FindAll
                (x => (x.UserFirstId == user.Id || x.UserSecondId == user.Id) && x.Status == AppConstants.FriendStatus.Accepted)
                .Count();

        top5FriendInfo = friendRepository.FindAll
                (x =>
                    ((x.UserFirstId == user.Id || x.UserSecondId == user.Id) && x.Status == AppConstants.FriendStatus.Accepted),
                 x => x.UserFirst,
                 x => x.UserSecond
                )
                .OrderBy(x => x.CreatedDate)
                .Take(5)
                .Select(x => new Response.UserPublicInfo.FriendInfo
                {
                    Id = x.UserFirstId == user.Id ? x.UserSecond!.Id : x.UserFirst!.Id,
                    Username = x.UserFirstId == user.Id ? x.UserSecond!.Username : x.UserFirst!.Username,
                    Fullname = x.UserFirstId == user.Id ? x.UserSecond!.Fullname : x.UserFirst!.Fullname,
                    Avatar = x.UserFirstId == user.Id ? x.UserSecond!.Avatar : x.UserFirst!.Avatar,
                })
                .ToList();

        result = result with
        {
            TotalFriends = totalFriend,
            Friends = top5FriendInfo
        };

        if (request.Id == null || request.Id == user.Id)
            return Result.Success(result);

        var friendRelationship = await friendRepository.FindSingleAsync
                (x =>
                    (x.UserFirstId == user.Id && x.UserSecondId == request.Id) ||
                    (x.UserSecondId == user.Id && x.UserFirstId == request.Id)
                );
        if (friendRelationship == null)
            return Result.Success(result);

        //blocked
        if (friendRelationship.Status == AppConstants.FriendStatus.Block && request.Id != friendRelationship.SpecifierId)
            return Result.Success(new Response.UserPublicInfo() { RelationshipStatus = AppConstants.FriendStatus.Block });

        int relationshipStatus = friendRelationship.Status;
        
        if (friendRelationship.Status == AppConstants.FriendStatus.Block && request.Id == friendRelationship.SpecifierId)
            relationshipStatus = AppConstants.FriendRealtionship.BlockRequester;

        if (friendRelationship.SpecifierId != request.Id && friendRelationship.Status == AppConstants.FriendStatus.WatitingAccept)
            relationshipStatus = AppConstants.FriendRealtionship.FriendRequest;

        result = result with { RelationshipStatus = relationshipStatus };

        return Result.Success(result);
    }
}
