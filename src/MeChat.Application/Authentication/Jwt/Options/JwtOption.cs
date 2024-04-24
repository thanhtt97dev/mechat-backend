namespace MeChat.Application.Authentication.Jwt.Options;

public class JwtOption
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpireMinute { get; set; }
}
