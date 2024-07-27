using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Persistence.DependencyInjection.Options;
using MeChat.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MeChat.Persistence.DependencyInjection.Extentions;
public static class ServiceCollectionExtensions
{
    public static void AddSqlServerEntityFramwork(this IServiceCollection services)
    {
        //add EF
        services.AddDbContextPool<DbContext, ApplicationDbContext>((serviceProvider, dbContextOptionbuilder) =>
        {
            var configration = serviceProvider.GetRequiredService<IConfiguration>();
            var options = serviceProvider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();

            dbContextOptionbuilder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true)
            .UseSqlServer(
                connectionString: configration.GetConnectionString("SqlServer"),
                sqlServerOptionsAction: optionsBuilder => 
                    optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
        });

        //add DI
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    }
}
