using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Middlewares;

namespace MeChat.Common.UseCases.V1.Auth;
public class Query
{
    public record SignIn(string Username, string Password) : IQuery<Response.Authenticated>;

    public record SignInByGoogle(Guid UserId, string GoogleToken) : IQuery<Response.Authenticated>, IDbTransactionMiddleware;

    public record RefreshToken(string? AccessToken, string? Refresh, string? UserId) : IQuery<Response.Authenticated>;
}
