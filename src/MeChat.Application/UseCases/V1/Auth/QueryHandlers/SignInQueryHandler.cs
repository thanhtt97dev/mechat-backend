using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class SignInQueryHandler : IQueryHandler<Query.SignIn, Response.Authenticated>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly AuthUtil authUtil;

    public SignInQueryHandler(
        IUnitOfWork unitOfWork, 
        AuthUtil authUtil)
    {
        this.unitOfWork = unitOfWork;
        this.authUtil = authUtil;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.SignIn request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetUserByUsernameAndPassword(request.Username, request.Password);
        
        if (user == null)
            //return Result.NotFound<Response.Authenticated>("Username or Password incorrect!");
            return Result.Success<Response.Authenticated>(null);

        //Check User
        if (user.Status != AppConstants.User.Status.Activate)
            return Result.Initialization<Response.Authenticated>(AppConstants.ResponseCodes.User.Banned, "User has been banned!", false, null);

        return await authUtil.GenerateToken(user);
    }
}
