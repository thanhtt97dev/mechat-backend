using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Authentication;
using MeChat.Infrastucture.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MeChat.Infrastucture.Service.DependencyInjection.Extentions;
public static class JwtExtention
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new ApplicationTokenValidationParameters(configuration);
            options.Events = new ApplicationJwtBearerValidationEvents();
        });

        services.AddAuthorization();
    }

    public static void AddJwtService(this IServiceCollection services)
    {
        services.AddTransient<IJwtService, JwtService>();
    }
}
