namespace MeChat.Common.Shared.Exceptions.Base;
public class UnAuthorizedException : DomainException
{
    public UnAuthorizedException(string message) : base("Un Authorized", message)
    {
    }
}
