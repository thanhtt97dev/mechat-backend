namespace MeChat.Common.UseCases.V1.User;
public class Response
{
    public record User(Guid Id, string RoleId, string Email, string Avatar, string Fullname, string Username);
}
