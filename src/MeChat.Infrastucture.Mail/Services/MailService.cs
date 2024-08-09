using MailKit.Net.Smtp;
using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Service.Email.Options;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MeChat.Infrastucture.Service.Email.Services;

public class MailService : IMailService
{
    private readonly EmailConfiguration emailOption = new();

    public MailService(IConfiguration configuration)
    {
        configuration.GetSection(nameof(EmailConfiguration)).Bind(emailOption);
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
        message.From.Add(new MailboxAddress("email", emailOption.Email));
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
            client.Connect(emailOption.SmtpServer, emailOption.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(emailOption.Email, emailOption.Password);

            await client.SendAsync(message);
        }
        catch(Exception) 
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
