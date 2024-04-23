using MeChat.Persistence.DependencyInjection.Extentions;
using MeChat.Infrastucture.Dapper.DependencyInjection.Extentions;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using MeChat.API.DependencyInjection.Extentions;
using MeChat.Application.DependencyInjection.Extentions;
using MeChat.API.Middlewares;

namespace MeChat.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();

        //Add db sql server
        builder.Services.AddDbSqlServerConfiguration();

        //Add infrastucture dapper
        builder.Services.AddConfigurationDapper();

        //Add configuration MediatR
        builder.Services.AddConfiguraionMediatR();

        //Add controller API
        builder.Services
            .AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        //Add config Swagger with api versioning
        builder.Services
            .AddSwaggerGenNewtonsoftSupport()
            .AddFluentValidationRulesToSwagger()
            .AddEndpointsApiExplorer()
            .AddConfigurationSwagger();

        //Add config Api versioning
        builder.Services
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        //Add config AutoMapper
        builder.Services.AddConfigurationAutoMapper();

        //Add middlewares
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

        //Add configuration Jwt
        builder.AddJwtAuthentication(builder.Configuration);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseConfigurationSwagger();
        }

        //Use middlewares
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}