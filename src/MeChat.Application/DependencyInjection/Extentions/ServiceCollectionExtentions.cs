﻿using FluentValidation;
using MeChat.Application.Behaviors;
using MeChat.Application.Mapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Application.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddConfiguraionMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        //Add MediatR's Middleware for Fluent Validation models
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        //Add Fluent Validation from Common Assembly
        services.AddValidatorsFromAssembly(Common.AssemblyReference.Assembly, includeInternalTypes: true);
    }
       
    public static void AddConfigurationAutoMapper(this IServiceCollection services) 
    {
        services.AddAutoMapper(typeof(ServiceProfile));
    }

}
