using MeChat.Common.Shared.Exceptions.Base;

namespace MeChat.Common.Shared.Exceptions;
public class AwsS3Exceptions
{
    public class NotFound : NotFoundException
    {
        public NotFound(string id) :
            base($"The file with the id {id} was not found.")
        { }

    }
}
