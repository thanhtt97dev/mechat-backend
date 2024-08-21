using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MeChat.Infrastucture.Service.DependencyInjection.Configurations;
using MeChat.Common.Abstractions.Services;

namespace MeChat.Infrastucture.Service.Services;
public class EmailService : IEmailService
{
    private readonly EmailConfiguration emailConfiguration = new();

    public EmailService(IConfiguration configuration)
    {
        configuration.GetSection(nameof(EmailConfiguration)).Bind(emailConfiguration);
    }

    public async Task SendMailAsync(IEnumerable<string> emails, string subject, string content)
    {
        var message = CreateEmailMessage(emails, subject, content);
        await SendAsync(message);
    }

    public async Task SendMailAsync(string email, string subject, string content)
    {
        var emails = new List<string>() { email };
        var message = CreateEmailMessage(emails, subject, content);
        await SendAsync(message);
    }

    private MimeMessage CreateEmailMessage(IEnumerable<string> emails, string subject, string content)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("email", emailConfiguration.Email));
        message.To.AddRange(emails.Select(x => new MailboxAddress("mail", x)));
        message.Subject = subject;
        message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = content };
        return message;
    }

    private async Task SendAsync(MimeMessage message)
    {
        var client = new SmtpClient();
        try
        {
            client.Connect(emailConfiguration.SmtpServer, emailConfiguration.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(emailConfiguration.Email, emailConfiguration.Password);

            await client.SendAsync(message);
        }
        catch (Exception)
        {
            throw new Exception("Cannot send mail!");
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }

    }
}
