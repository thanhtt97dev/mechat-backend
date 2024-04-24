using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Exceptions;
using MeChat.MeChat.Infrastucture.Jwt.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeChat.Infrastucture.Jwt.Extentions;
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOption jwtOption = new JwtOption();

    public JwtTokenService(IConfiguration configuration)
    {
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKeyBytes = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
        var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Issuer = jwtOption.Issuer,
            Audience = jwtOption.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(jwtOption.ExpireMinute),
            SigningCredentials = signingCredentials
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescription);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
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

            var tokenValidationPrarameters = new TokenValidationParameters
            {
                ValidateIssuer = false, //on production make it true
                ValidateAudience = false, //on production make it true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOption.Issuer,
                ValidAudience = jwtOption.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };


            var tokenhandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenhandler.ValidateToken(token, tokenValidationPrarameters, out SecurityToken securityToken);

            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return claimsPrincipal;
        }
        catch (Exception ex) 
        {
            return null;
        }
    }
}
