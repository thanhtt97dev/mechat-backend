namespace MeChat.Infrastucture.Jwt.DependencyInjection.Options;
internal class JwtOption
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public int ExpireMin { get; set; }
    public string EncryptingKey { get;set; }
}
