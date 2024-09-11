using AutoMapper;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.QueryHandlers;
public class GetUserPublicInfoQueryHandler : IQueryHandler<Query.GetUserPublicInfo, Response.UserPublicInfo>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IMapper mapper;

    public GetUserPublicInfoQueryHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
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
        return Result.Success(result);
    }
}
