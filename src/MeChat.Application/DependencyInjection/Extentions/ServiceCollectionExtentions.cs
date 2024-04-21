using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Contracts;

namespace MeChat.Application.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddConfiguraionMediatR(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(AssemblyReference.Assembly);
        });

        //Add pipe line behavior

        //Add validation
        services.AddValidatorsFromAssembly(Common.AssemblyReference.Assembly, includeInternalTypes: true);
    }

}
