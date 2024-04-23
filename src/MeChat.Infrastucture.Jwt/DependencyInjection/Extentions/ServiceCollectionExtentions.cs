using Demo.Infrastucture.Authentication;
using MeChat.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Jwt.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddJwtServices(this IServiceCollection services)
    {
        services.AddTransient<IJwtTokenService, JwtTokenService>();
    }
}
