using MeChat.Common.Abstractions.Messages;

namespace MeChat.Common.UseCases.V1.Auth;
public class Query
{
    public record Login(string Username, string Password) : IQuery<Response.Authenticated>;
}
