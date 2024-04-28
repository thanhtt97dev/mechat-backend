namespace MeChat.Infrastucture.Mail.DependencyInjection.Options;
public class EmailOption
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? SmtpServer { get; set; }
    public int Port { get; set; }
}
