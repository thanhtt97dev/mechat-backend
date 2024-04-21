namespace MeChat.Common.UseCases.V1.User;
public class Response
{
    public record User(Guid Id, string Username, string Password);
}
