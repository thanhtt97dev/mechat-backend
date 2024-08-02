using MeChat.Common.Abstractions.Messages;

namespace MeChat.Common.UseCases.V1.Auth;
public class Command
{
    public record SignUp(string Username, string Password, string Email) : ICommand;
    public record ConfirmSignUp(string AccessToken) : ICommand;
}
