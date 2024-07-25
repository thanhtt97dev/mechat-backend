using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static MeChat.Common.Shared.Exceptions.AuthExceptions;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class GetRefreshTokenQueryHandler : IQueryHandler<Query.RefreshToken, Response.Authenticated>
{
    private readonly ICacheService cacheService;
    private readonly IJwtTokenService jwtTokenService;
    private readonly IUnitOfWork unitOfWork;
    private readonly AuthUtil authUtil;

    public GetRefreshTokenQueryHandler(
        ICacheService cacheService, 
        IJwtTokenService jwtTokenService, 
        IUnitOfWork unitOfWork, 
        AuthUtil authUtil)
    {
        this.cacheService = cacheService;
        this.jwtTokenService = jwtTokenService;
        this.unitOfWork = unitOfWork;
        this.authUtil = authUtil;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.RefreshToken request, CancellationToken cancellationToken)
    {
        //check request is valid
        if(request.UserId == null || request.AccessToken == null)
            throw new AccessTokenInValid();

        //get user Id in acces token
        var rawUserId = jwtTokenService.GetClaim(AppConfiguration.Jwt.ID, request.AccessToken);
        if (rawUserId == null) throw new AccessTokenInValid();
#pragma warning disable CS8604 // Possible null reference argument.
        Guid userId = Guid.Parse(rawUserId.ToString());
#pragma warning restore CS8604 // Possible null reference argument.

        //check userId in request header with userId in accessToken is match
        if (userId.ToString() != request.UserId)
            throw new AccessTokenInValid();

        //get refresh token in access token
        var rawRefreshToken = jwtTokenService.GetClaim(AppConfiguration.Jwt.JTI, request.AccessToken);
        if (rawRefreshToken == null) throw new AccessTokenInValid();
        var refreshTokenInAccessToken = (string)rawRefreshToken;

        //check refresh token in request match with refresh token in access token
        if(request.Refresh != refreshTokenInAccessToken)
            throw new AccessTokenInValid();

        //Check user's permitssion
        var user = await unitOfWork.Users.FindByIdAsync(userId) ?? throw new UserNotHavePermission();

        if (user.Status != Common.Constants.UserConstant.Status.Activate)
            return Result.Initialization<Response.Authenticated>(ResponseCodes.UserBanned, "User has been banned!", false, null);

        //Check refesh token
        var rawUserIdFromCacheWithRefreshToken = await cacheService.GetCache(request.Refresh!) ?? string.Empty;
        if(string.IsNullOrEmpty(rawUserIdFromCacheWithRefreshToken))
            return Result.Failure<Response.Authenticated>(null, "Refresh token has been expried!");

        var userIdFromCacheWithRefreshToken = JsonConvert.DeserializeObject<string>(rawUserIdFromCacheWithRefreshToken);
        if (userIdFromCacheWithRefreshToken != user.Id.ToString())
            return Result.Failure<Response.Authenticated>(null, "Invalid refresh token!");

        //Remove old refresh token from cache
        await cacheService.RemoveCache(request.Refresh!);

        return await authUtil.GenerateToken(user.Id, user.RoldeId, user.Email);
    }

}
