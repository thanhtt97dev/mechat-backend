using Azure.Core;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Exceptions;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Infrastucture.Jwt.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;

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
            throw new AuthExceptions.AccessTokenInValid();

        Guid userId = GetUserIdByAccessToken(request.AccessToken);

        //check userId in request header with userId in accessToken is match
        if (userId.ToString() != request.UserId)
            throw new AuthExceptions.AccessTokenInValid();

        //Check user's permitssion
        var user = await unitOfWork.Users.FindByIdAsync(userId) ?? throw new AuthExceptions.UserNotHavePermission();

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
        await cacheService.RemoveCache(request.Refresh);

        //Generate new access token
        var clamims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin"),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);
        var refreshToken = jwtTokenService.GenerateRefreshToken();

        JwtOption jwtOption = new();
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

        var sessionTime = jwtOption.ExpireMinute + jwtOption.RefreshTokenExpireMinute;

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddMinutes(sessionTime)
        };

        //save refresh token into cache
        await cacheService.SetCache(user.Id.ToString(), refreshToken, TimeSpan.FromMinutes(sessionTime));

        return Result.Success(result);
    }

    private Guid GetUserIdByAccessToken(string accessToken)
    {
        //Get userId in access token
        try
        {
            var principal = jwtTokenService.GetClaimsPrincipal(accessToken);
            var userIdFromAccessToken = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name) ?? throw new AuthExceptions.AccessTokenInValid();
            return new Guid(userIdFromAccessToken.Value);
        }
        catch (SecurityTokenInvalidLifetimeException)
        {
            throw new AuthExceptions.AccessTokenExpried();
        }
        catch (SecurityTokenException)
        {
            throw new AuthExceptions.AccessTokenInValid();
        }
    }
}
