using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Infrastucture.Service.DependencyInjection.Configurations;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace MeChat.Application.UseCases.V1.Auth.Utils;
public class AuthUtil
{
    private readonly IConfiguration configuration;
    private readonly ICacheService cacheService;
    private readonly IJwtService jwtTokenService;

    public AuthUtil(IConfiguration configuration, ICacheService cacheService, IJwtService jwtTokenService)
    {
        this.configuration = configuration;
        this.cacheService = cacheService;
        this.jwtTokenService = jwtTokenService;
    }

    public async Task<Result<Response.Authenticated>> GenerateToken(Domain.Entities.User user)
    {
        JwtConfiguration jwtConfiguration = new();
        configuration.GetSection(nameof(JwtConfiguration)).Bind(jwtConfiguration);
        var sessionTime = jwtConfiguration.ExpireMinute + jwtConfiguration.RefreshTokenExpireMinute;

        var refreshToken = jwtTokenService.GenerateRefreshToken();

        var clamims = new List<Claim>
        {
            new Claim(AppConstants.AppConfigs.Jwt.ID, user.Id.ToString()),
            new Claim(AppConstants.AppConfigs.Jwt.ROLE, user.RoleId.ToString()),
            new Claim(AppConstants.AppConfigs.Jwt.EMAIL, user.Email??string.Empty),
            new Claim(AppConstants.AppConfigs.Jwt.JTI, refreshToken),
            new Claim(AppConstants.AppConfigs.Jwt.EXPIRED, DateTime.Now.AddMinutes(jwtConfiguration.ExpireMinute).ToString()),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            //RefreshTokenExpiryTime = DateTime.Now.AddMinutes(sessionTime),
            UserId = user.Id.ToString(),
            UserName = user.Username,
            Fullname = user.Fullname,
            RoleId = user.RoleId,
            Avatar = user.Avatar ?? string.Empty
        };

        //save refresh token into cache
        await cacheService.SetCache(refreshToken, user.Id.ToString(), TimeSpan.FromMinutes(sessionTime));

        return Result.Success(result);
    }

    public string GenerateTokenForSignUp(string email)
    {
        JwtConfiguration jwtConfiguration = new();
        configuration.GetSection(nameof(JwtConfiguration)).Bind(jwtConfiguration);

        var clamims = new List<Claim>
        {
            new Claim(AppConstants.AppConfigs.Jwt.EMAIL, email??string.Empty),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);

        return accessToken;
    }
}
