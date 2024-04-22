using MeChat.Common.Shared.Response;
using MeChat.Common.Shared.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MeChat.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }catch(Exception ex) 
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = GetResultException(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static Result GetResultException(Exception exception) 
    {
        Result result;
        switch(exception) 
        {
            case BadRequestException:
                result = Result.Failure(exception.Message);
                break;
            case NotFoundException:
                result = Result.NotFound(exception.Message);
                break;
            case FluentValidation.ValidationException:
                result = Result.ValidationError(exception.Message);
                break;
            case UnAuthorizedException:
                result = Result.UnAuthorized(exception.Message);
                break;
            case FormatException:
                result = Result.ValidationError(exception.Message);
                break;
            default:
                result = Result.Failure(exception.Message);
                break;
        }
        return result;
    }

    private static int GetStatusCode(Exception exception)
    =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            FluentValidation.ValidationException => StatusCodes.Status422UnprocessableEntity,
            UnAuthorizedException => StatusCodes.Status401Unauthorized,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            DomainException applicationException => applicationException.Title,
            _ => "Server Error"
        };

}
