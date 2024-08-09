﻿using MeChat.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Application.DependencyInjection.Extentions;
public static class MapperObjectExtention
{
    public static void AddConfigAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceProfile));
    }
}
