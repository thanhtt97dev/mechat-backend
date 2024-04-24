using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Exceptions;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.MeChat.Infrastucture.Jwt.Options;
using Microsoft.Extensions.Configuration;
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
        //Get userId in access token
        var principal = jwtTokenService.GetClaimsPrincipal(request.AccessToken);
        var userIdFromAccessToken = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
        if (userIdFromAccessToken == null) throw new AuthExceptions.AccessTokenInValid();
        var userId = new Guid(userIdFromAccessToken.Value);

        //Check user's permitssion
        var user = await unitOfWork.Users.FindByIdAsync(userId);
        if(user == null) // check user is banned yet
            throw new AuthExceptions.UserNotHavePermission();

        //Check refesh token
        var refreshTokenInCache = await cacheService.GetCache(userId.ToString());
        if (refreshTokenInCache == null || refreshTokenInCache != request.Refresh)
            return Result.Failure<Response.Authenticated>(null, "Refresh token has been expried!");

        //Generate new access token
        var clamims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin"),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);
        var refreshToken = jwtTokenService.GenerateRefreshToken();

        JwtOption jwtOption = new JwtOption();
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
}
