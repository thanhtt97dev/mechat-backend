using MeChat.Common.Abstractions.Messages;

namespace MeChat.Common.UseCases.V1.User;
public class Query
{
    public record GetUserById(Guid Id) : IQuery<Response.User>;
}
