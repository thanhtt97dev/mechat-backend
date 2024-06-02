namespace MeChat.Common.Shared.Exceptions.Base;
public class UnAuthenticationException : DomainException
{
    protected UnAuthenticationException(string message) : base("UnAutentication", message)
    {
    }
}
