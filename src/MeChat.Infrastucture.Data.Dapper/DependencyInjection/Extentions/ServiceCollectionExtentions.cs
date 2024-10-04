using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Infrastucture.Dapper.Repositories;
using MeChat.Infrastucture.Data.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Dapper.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddSqlServerDapper(this IServiceCollection services)
    {
        services.AddTransient<ApplicationDbContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<INotificationRepository, NotificationRepository>();
        services.AddTransient<IUserSocialRepository, UserSocialRepository>();
    }
}
