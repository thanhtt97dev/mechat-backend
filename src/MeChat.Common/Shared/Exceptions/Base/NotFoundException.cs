namespace MeChat.Common.Shared.Exceptions.Base;
public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base("Not Found", message)
    {
    }
}
