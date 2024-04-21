using MeChat.Common.Shared.Exceptions.Base;

namespace MeChat.Common.Shared.Exceptions;
public static class UserExceptions
{
    public class UserNotFound : NotFoundException
    {
        protected UserNotFound(Guid id) :
            base($"The user with the id {id} was not found.") { }

    }
}
