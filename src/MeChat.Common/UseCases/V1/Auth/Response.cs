namespace MeChat.Common.UseCases.V1.Auth;
public static class Response
{
    public class Authenticated : UserInfo
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        
    }

    public class UserInfo
    {
        public string? UserId { get; set; }
        public string? Fullname { get; set; }
        public int RoleId { get; set; }
    }
}
