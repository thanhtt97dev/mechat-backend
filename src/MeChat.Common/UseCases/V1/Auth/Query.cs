using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.Messages.DomainEvents.Annotations;

namespace MeChat.Common.UseCases.V1.Auth;
public class Query
{
    public record SignIn(string Username, string Password) : IQuery<Response.Authenticated>;

    public record SignInByGoogle(string GoogleToken) : IQuery<Response.Authenticated>, IDbTransactionAnnotation;

    public record RefreshToken(string? AccessToken, string? Refresh, string? UserId) : IQuery<Response.Authenticated>;
}
