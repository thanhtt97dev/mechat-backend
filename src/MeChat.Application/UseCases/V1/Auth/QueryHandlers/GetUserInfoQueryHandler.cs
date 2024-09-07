using AutoMapper;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class GetUserInfoQueryHandler : IQueryHandler<Query.UserInfo, Response.UserInfo>
{

    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IMapper mapper;

    public GetUserInfoQueryHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository,
        IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Response.UserInfo>> Handle(Query.UserInfo request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId, default, (x => x.Role));

        if(user == null)
            return Result.NotFound<Response.UserInfo>("User not found!");

        if (user.Status != AppConstants.User.Status.Activate)
            return Result.UnAuthentication<Response.UserInfo>("Unauthenticated");

        var result = mapper.Map<Response.UserInfo>(user);

        return Result.Success(result);
    }
}
