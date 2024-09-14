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
        Guid.TryParse(request.Key,out userId);

        var user = await userRepository
                .FindSingleAsync(x => x.Id == userId || x.Username == request.Key || x.Email == request.Key);
        if (user == null)
            return Result.NotFound<Response.UserPublicInfo>("User not found!");

        var result = mapper.Map<Response.UserPublicInfo>(user);

        if(request.Id == null || request.Id == user.Id)
            return Result.Success(result);

        var friend = await friendRepository.FindSingleAsync
                (x =>
                    (x.UserFirstId == user.Id && x.UserSecondId == request.Id) ||
                    (x.UserSecondId == user.Id && x.UserFirstId == request.Id)
                );
        if (friend == null)
            return Result.Success(result);

        int relationshipStatus = friend.Status;
        if (friend.SpecifierId == request.Id && friend.Status == AppConstants.FriendStatus.WatitingAccept)
            relationshipStatus = AppConstants.FriendRealtionship.FriendRequest;

        result = result with { RelationshipStatus = relationshipStatus };

        return Result.Success(result);
    }
}
