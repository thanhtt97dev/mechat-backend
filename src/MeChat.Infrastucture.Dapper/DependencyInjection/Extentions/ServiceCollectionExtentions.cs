using MeChat.Domain.Abstractions.Dapper;
using MeChat.Domain.Abstractions.Dapper.Repositories;
using MeChat.Infrastucture.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Dapper.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddConfigurationDapper(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ApplicationDbContext>();

        services.AddTransient<IUserRepository, UserRepository>();
    }
}
