using MeChat.API.Authentication.Jwt.Abstractions;
using MeChat.API.Authentication.Jwt.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MeChat.API.Authentication.Jwt.Extentions;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOption jwtOption = new JwtOption();

    public JwtTokenService(IConfiguration configuration)
    {
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));
        var encryptingCredentials = 
            new EncryptingCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOption.SecretKey)), 
                JwtConstants.DirectKeyUseAlg, 
                SecurityAlgorithms.Aes256CbcHmacSha512);
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = jwtOption.Issuer,
            Audience = jwtOption.Audience,
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.Now,
            Expires = DateTime.Now.AddSeconds(jwtOption.ExpireSecond),
            IssuedAt = DateTime.Now,
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
        };

        var jwtSecurityToken = jwtSecurityTokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);

        var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var random = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(random);
        var randomNumber = Convert.ToBase64String(random);

        return randomNumber;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));
        var encryptingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.EncryptingKey));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, //on production make it true
            ValidateAudience = false, //on production make it true
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOption.Issuer,
            ValidAudience = jwtOption.Audience,
            IssuerSigningKey = secretKey,
            ClockSkew = TimeSpan.Zero,
            TokenDecryptionKey = encryptingCredentials,
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return claimsPrincipal;
    }
}
