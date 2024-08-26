using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class GetUserInfoQueryHandler : IQueryHandler<Query.UserInfo, Response.UserInfo>
{

    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    public GetUserInfoQueryHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository
        )
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<Response.UserInfo>> Handle(Query.UserInfo request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId, default, (x => x.Role));

        if(user == null)
            return Result.NotFound<Response.UserInfo>("User not found!");

        if (user.Status != AppConstants.User.Status.Activate)
            return Result.UnAuthentication<Response.UserInfo>("Unauthenticated");

        var result = new Response.UserInfo
        {
            UserId = request.UserId.ToString(),
            Fullname = user.Fullname,
            RoleId = user.RoldeId
        };

        return Result.Success(result);
    }
}
