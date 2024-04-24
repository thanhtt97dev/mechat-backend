using System.Security.Claims;

namespace MeChat.Common.Abstractions.Services;
public interface IJwtTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimsPrincipal(string token);
}
