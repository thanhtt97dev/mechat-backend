using MeChat.Common.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MeChat.Common.Shared.Authentication;
public class ApplicationJwtBearerValidationEvents : JwtBearerEvents
{
    public override Task MessageReceived(MessageReceivedContext context)
    {
        var path = context.HttpContext.Request.Path.ToString();
        if (path.Contains(AppConstants.RealTime.Endpoint.Root))
        {
            var accessToken = context.Request.Query["access_token"];
            context.Token = accessToken;
        }
        return Task.CompletedTask;
    }

    public override Task TokenValidated(TokenValidatedContext context)
    {
        return Task.CompletedTask;
    }

    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        {
            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
        }
        return base.AuthenticationFailed(context);
    }
}
