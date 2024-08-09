using FluentValidation;
using MeChat.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Application.DependencyInjection.Extentions;
public static class MediatorExtention
{
    public static void AddConfigMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        //Add MediatR's Middleware for Fluent Validation models
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        //Add MediatR's Middleware for Global transaction EF
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));

        //Add Fluent Validation from Common Assembly
        services.AddValidatorsFromAssembly(Common.AssemblyReference.Assembly, includeInternalTypes: true);
    }
}
