namespace MeChat.Common.Shared.ApplicationConfiguration;
public class Jwt
{
    public bool ValidateIssuer { get; set; } = false;
    public bool ValidateAudience { get; set; } = false;
    public bool ValidateLifetime { get; set; } = false;
    public bool ValidateIssuerSigningKey { get; set; } = false;
    public string ValidIssuer { get; set; } = string.Empty;
    public string ValidAudience { get; set; } = string.Empty;
    public string IssuerSigningKey { get; set; } = string.Empty;
    public int ClockSkew { get; set; } = 0;
    public int ExpireMinute { get; set; } = 0;
    public int RefreshTokenExpireMinute { get; set;} = 0;
}
