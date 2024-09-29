using MeChat.Common.Shared.ApplicationConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MeChat.Common.Shared.Authentication;
public class ApplicationTokenValidationParameters : TokenValidationParameters
{
    public ApplicationTokenValidationParameters(IConfiguration configuration)
    {
        Jwt jwtConfig = new();
        configuration.GetSection(nameof(Jwt)).Bind(jwtConfig);

        ValidateIssuer = jwtConfig.ValidateIssuer;//on production make it true
        ValidateAudience = jwtConfig.ValidateAudience; //on production make it true
        ValidateLifetime = jwtConfig.ValidateAudience;
        ValidateIssuerSigningKey = jwtConfig.ValidateIssuerSigningKey;
        ValidIssuer = jwtConfig.ValidIssuer;
        ValidAudience = jwtConfig.ValidAudience;
        var key = Encoding.UTF8.GetBytes(jwtConfig.IssuerSigningKey);
        IssuerSigningKey = new SymmetricSecurityKey(key);
        ClockSkew = new TimeSpan(jwtConfig.ClockSkew);
    }
}
