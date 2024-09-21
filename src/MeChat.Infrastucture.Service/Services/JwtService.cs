using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.ApplicationConfiguration;
using MeChat.Common.Shared.Authentication;
using MeChat.Common.Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeChat.Infrastucture.Service.Services;
internal class JwtService : IJwtService
{
    private readonly JwtConfiguration jwtConfiguration = new();
    private readonly IConfiguration configuration;

    public JwtService(IConfiguration configuration)
    {
        this.configuration = configuration;
        configuration.GetSection(nameof(JwtConfiguration)).Bind(jwtConfiguration);
    }

    #region Generate Accsess Token
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.IssuerSigningKey));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            issuer: jwtConfiguration.ValidIssuer,
            audience: jwtConfiguration.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtConfiguration.ExpireMinute),
            signingCredentials: signinCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return token;
    }
    #endregion

    #region Generate Refresh Token
    public string GenerateRefreshToken()
    {
        Random random = new Random();
        string guid = Guid.NewGuid().ToString();
        string result = $"{random.Next()}{guid}{random.Next()}";
        return result;
    }
    #endregion

    #region Get Claims Principal
    private ClaimsPrincipal GetClaimsPrincipal(string? token, bool validateLifetime)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
                throw new AuthExceptions.AccessTokenInValid();

            var tokenValidationParameters = new ApplicationTokenValidationParameters(configuration);

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        catch (Exception)
        {
            throw new SecurityTokenInvalidLifetimeException("Invalid token");
        }
    }
    #endregion

    #region Get Claim
    public object? GetClaim(string? claimType, string? token)
    {
        if (claimType is null || token is null)
            return null;
        var principal = GetClaimsPrincipal(token, true);
        var result = principal.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        return result;
    }

    public object? GetClaim(string? claimType, string? token, bool validateLifetime)
    {
        if (claimType is null || token is null)
            return null;
        var principal = GetClaimsPrincipal(token, validateLifetime);
        var result = principal.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        return result;
    }
    #endregion

    #region Validate AccessToken
    public bool ValidateAccessToken(string accessToken, bool validateLifetime)
    {
        if (string.IsNullOrEmpty(accessToken))
            throw new AuthExceptions.AccessTokenInValid();

        var tokenValidationParameters = new ApplicationTokenValidationParameters(configuration);

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return false;
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
    #endregion

}