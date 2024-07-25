using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Constants;
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
            return Result.Failure<Response.Authenticated>(null, "Username or Password incorrect!");

        //Check User
        if (user.Status != UserConstant.Status.Activate)
            return Result.Initialization<Response.Authenticated>(ResponseCodes.UserBanned, "User has been banned!", false, null);

        return await authUtil.GenerateToken(user.Id, user.RoldeId, user.Email);
    }
}
