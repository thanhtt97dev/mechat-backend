namespace MeChat.Infrastucture.Service.DependencyInjection.Configurations;
public class JwtConfiguration
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpireMinute { get; set; }
    public int RefreshTokenExpireMinute { get; set; }
}
