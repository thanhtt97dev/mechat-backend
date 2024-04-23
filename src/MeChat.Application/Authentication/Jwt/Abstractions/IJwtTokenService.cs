using System.Security.Claims;

namespace MeChat.Application.Authentication.Jwt.Abstractions;
public interface IJwtTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
