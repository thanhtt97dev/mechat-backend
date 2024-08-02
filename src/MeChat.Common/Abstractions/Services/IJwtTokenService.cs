using System.Security.Claims;

namespace MeChat.Common.Abstractions.Services;
public interface IJwtTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    object? GetClaim(string? claimType, string? token);
    object? GetClaim(string? claimType, string? token, bool validateLifetime);
    bool ValidateAccessToken(string accessToken, bool validateLifetime);
}
