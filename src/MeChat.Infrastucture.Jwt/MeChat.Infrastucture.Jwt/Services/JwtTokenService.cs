using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Exceptions;
using MeChat.Infrastucture.Jwt.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeChat.Infrastucture.Jwt.Extentions;
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOption jwtOption = new();

    public JwtTokenService(IConfiguration configuration)
    {
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            issuer: jwtOption.Issuer,
            audience: jwtOption.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtOption.ExpireMinute),
            signingCredentials: signinCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return token;
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public ClaimsPrincipal GetClaimsPrincipal(string? token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
                throw new AuthExceptions.AccessTokenInValid();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateLifetime = true, //here we are saying that we don't care about the token's expiration date
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

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
}
