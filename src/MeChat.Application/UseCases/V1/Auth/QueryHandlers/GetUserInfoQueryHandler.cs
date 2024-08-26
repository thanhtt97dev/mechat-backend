using Azure.Core;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class GetUserInfoQueryHandler : IQueryHandler<Query.UserInfo, Response.Authenticated>
{

    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    public GetUserInfoQueryHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository
        )
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.UserInfo request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId);

        if(user == null) 
        {
            return Result.NotFound<Response.Authenticated>("User not found!");
        }

        if(user.Status != AppConstants.User.Status.Activate)
        {
            return Result.UnAuthentication<Response.Authenticated>("Unauthenticated");
        }

        var result = new Response.Authenticated
        {
            UserId = request.UserId.ToString(),
            Fullname = user.Fullname,
            RoleId = user.RoldeId
        };

        return Result.Success(result);
    }
}
