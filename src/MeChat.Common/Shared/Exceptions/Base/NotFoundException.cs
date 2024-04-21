namespace MeChat.Common.Shared.Exceptions.Base;
public class NotFoundException : DomainException
{
    protected NotFoundException(string message) : base("Not Found", message)
    {
    }
}
