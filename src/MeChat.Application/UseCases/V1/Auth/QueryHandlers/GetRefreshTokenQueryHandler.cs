using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Exceptions;
using MeChat.Common.Shared.Exceptions.Base;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Infrastucture.Jwt.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static MeChat.Common.Shared.Exceptions.AuthExceptions;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class GetRefreshTokenQueryHandler : IQueryHandler<Query.RefreshToken, Response.Authenticated>
{
    private readonly ICacheService cacheService;
    private readonly IJwtTokenService jwtTokenService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;

    public GetRefreshTokenQueryHandler(ICacheService cacheService, IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        this.cacheService = cacheService;
        this.jwtTokenService = jwtTokenService;
        this.unitOfWork = unitOfWork;
        this.configuration = configuration;
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

        //Generate new access token
        JwtOption jwtOption = new();
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
        var sessionTime = jwtOption.ExpireMinute + jwtOption.RefreshTokenExpireMinute;
        var refreshToken = jwtTokenService.GenerateRefreshToken();

        var clamims = new List<Claim>
        {
            new Claim(AppConfiguration.Jwt.ID, user.Id.ToString()),
            new Claim(AppConfiguration.Jwt.ROLE, user.RoldeId.ToString()),
            new Claim(AppConfiguration.Jwt.EMAIL, user.Email??string.Empty),
            new Claim(AppConfiguration.Jwt.JTI, refreshToken),
            new Claim(AppConfiguration.Jwt.EXPIRED, DateTime.Now.AddMinutes(jwtOption.ExpireMinute).ToString()),
        };
        var accessToken = jwtTokenService.GenerateAccessToken(clamims);

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddMinutes(sessionTime),
            UserId = userId.ToString()
        };

        //save refresh token into cache
        await cacheService.SetCache(refreshToken, user.Id.ToString(), TimeSpan.FromMinutes(sessionTime));

        return Result.Success(result);
    }

}
