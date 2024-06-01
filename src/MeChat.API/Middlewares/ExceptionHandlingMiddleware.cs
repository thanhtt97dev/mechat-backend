using MeChat.Common.Shared.Response;
using MeChat.Common.Shared.Exceptions.Base;
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
        var y = response.GetType().Name;

        var x = JsonSerializer.Serialize(response);

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static Result GetResultException(Exception exception) 
    {
        Result result = exception switch
        {
            BadRequestException => Result.Failure(exception.Message),
            NotFoundException => Result.NotFound(exception.Message),
            FluentValidation.ValidationException => Result.ValidationError(exception.Message),
            UnAuthorizedException => Result.UnAuthorized(exception.Message),
            FormatException => Result.ValidationError(exception.Message),
            _ => Result.Failure(exception.Message),
        };
        return result;
    }

    private static int GetStatusCode(Exception exception)
    =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            FluentValidation.ValidationException => StatusCodes.Status422UnprocessableEntity,
            UnAuthorizedException => StatusCodes.Status403Forbidden,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
}
