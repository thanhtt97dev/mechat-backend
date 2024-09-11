namespace MeChat.Common.UseCases.V1.User;
public class Response
{
    public record User(Guid Id, string RoleId, string Email, string Avatar, string Fullname, string Username);
    public record UserPublicInfo(Guid Id, string Username,int RoleId ,string FullName, string Avatar, string Email);
}
