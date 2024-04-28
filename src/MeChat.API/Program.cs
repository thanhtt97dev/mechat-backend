using MeChat.Persistence.DependencyInjection.Extentions;
using MeChat.Infrastucture.Dapper.DependencyInjection.Extentions;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using MeChat.API.DependencyInjection.Extentions;
using MeChat.Application.DependencyInjection.Extentions;
using MeChat.API.Middlewares;
using MeChat.Infrastucture.Jwt.DependencyInjection;
using MeChat.Infrastucture.Redis.DependencyInjection.Extentions;
using System.Text.Json.Serialization;
using MeChat.Infrastucture.Mail.DependencyInjection.Extentions;

namespace MeChat.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();

        //Add configuration Swagger with api versioning (API)
        builder.Services
            .AddSwaggerGenNewtonsoftSupport()
            .AddFluentValidationRulesToSwagger()
            .AddEndpointsApiExplorer()
            .AddConfigurationSwagger();

        //Add configuration Api versioning
        builder.Services
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        //Add configuration MediatR(Application)
        builder.Services.AddMediatR();

        //Add configuration AutoMapper(Application)
        builder.Services.AddAutoMapper();

        //Add configuration connect SQL Server with Dapper(Infrastucture.Dapper)
        builder.Services.AddSqlServerDapper();

        //Add configuration Jwt Authentication (Infrastucture.Jwt)
        builder.Services.AddJwtAuthentication(builder.Configuration);

        //Add configuration Jwt Service (Infrastucture.Jwt)
        builder.Services.AddJwtService();

        //Add configuration Mail service(Infrastucture.Mail)
        builder.Services.AddMailService();

        //Add configuration Redis(Infrastucture.Redis)
        builder.Services.AddCacheRedis(builder.Configuration);
        
        //Add configuration connect SQL Server with EF(Infrastucture.Persistence)
        builder.Services.AddSqlServerEntityFramwork();

        //Add controller API (Infrastucture.Presentation)
        builder.Services
            .AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        //Add Middlewares
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

        //Add Cors
        builder.Services.AddCors(options =>
        {
            var origin = builder.Configuration["CorsOrign"] ?? string.Empty;
            options.AddPolicy("FE-endpoint", builder =>
            {
                builder.WithOrigins(origin).AllowCredentials().AllowAnyMethod().AllowAnyHeader();
            });
        });

        // Use remove cycle object's data in json respone
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseConfigurationSwagger();
        }

        //Use CORS
        app.UseCors();

        //Use middlewares
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        //Use authentication
        app.UseAuthentication();

        //Use authorization
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}