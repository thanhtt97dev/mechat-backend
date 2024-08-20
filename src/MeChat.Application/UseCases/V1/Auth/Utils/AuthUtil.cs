using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Infrastucture.Service.Jwt.DependencyInjection.Options;
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

    public async Task<Result<Response.Authenticated>> GenerateToken(Guid id, int role, string? email)
    {
        JwtOption jwtOption = new();
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
        var sessionTime = jwtOption.ExpireMinute + jwtOption.RefreshTokenExpireMinute;

        var refreshToken = jwtTokenService.GenerateRefreshToken();

        var clamims = new List<Claim>
        {
            new Claim(AppConstants.AppConfigs.Jwt.ID, id.ToString()),
            new Claim(AppConstants.AppConfigs.Jwt.ROLE, role.ToString()),
            new Claim(AppConstants.AppConfigs.Jwt.EMAIL, email??string.Empty),
            new Claim(AppConstants.AppConfigs.Jwt.JTI, refreshToken),
            new Claim(AppConstants.AppConfigs.Jwt.EXPIRED, DateTime.Now.AddMinutes(jwtOption.ExpireMinute).ToString()),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddMinutes(sessionTime),
            UserId = id.ToString(),
        };

        //save refresh token into cache
        await cacheService.SetCache(refreshToken, id.ToString(), TimeSpan.FromMinutes(sessionTime));

        return Result.Success(result);
    }

    public string GenerateTokenForSignUp(string email)
    {
        JwtOption jwtOption = new();
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

        var clamims = new List<Claim>
        {
            new Claim(AppConstants.AppConfigs.Jwt.EMAIL, email??string.Empty),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);

        return accessToken;
    }
}
