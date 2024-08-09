namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Options;

public sealed class MasstransitConfiguration
{
    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string VHost { get; set; } = string.Empty;
}
