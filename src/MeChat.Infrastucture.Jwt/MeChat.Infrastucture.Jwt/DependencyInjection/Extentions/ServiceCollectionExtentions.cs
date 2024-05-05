using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Jwt.DependencyInjection.Options;
using MeChat.Infrastucture.Jwt.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MeChat.Infrastucture.Jwt.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
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
            JwtOption jwtOption = new();
            configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

            var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, //on production make it true
                ValidateAudience = false, //on production make it true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOption.Issuer,
                ValidAudience = jwtOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();
    }

    public static void AddJwtService(this IServiceCollection services)
    {
        services.AddTransient<IJwtTokenService, JwtTokenService>();
    }
}
