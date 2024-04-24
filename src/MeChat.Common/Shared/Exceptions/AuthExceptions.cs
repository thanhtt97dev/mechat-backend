using MeChat.Common.Shared.Exceptions.Base;

namespace MeChat.Common.Shared.Exceptions;
public class AuthExceptions
{
    public class AccessTokenInValid : BadRequestException
    {
        public AccessTokenInValid() :base($"Access token invalid!") { }
    }

    public class UserNotHavePermission : BadRequestException
    {
        public UserNotHavePermission() : base($"User not have permission!") { }
    }
}
