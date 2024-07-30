namespace MeChat.Common.Abstractions.Services;
public interface IMailService
{
    Task SendMailAsync(IEnumerable<string> emails, string subject, string content);
    Task SendMailAsync(string email, string subject, string content);
}
