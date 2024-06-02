using MeChat.Common.Shared.Response;
using MeChat.Common.Shared.Exceptions.Base;
using System.Text.Json;
using System.Text.Json.Serialization;
using MeChat.Common.Shared.Commons;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        var result = GetException(exception);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(result);
    }


    private static string GetException(Exception exception) 
    {
        object data = exception switch
        {
            BadRequestException => Result.Failure("Failure", exception.Message),
            NotFoundException => Result.NotFound(exception.Message),
            UnAuthorizedException => Result.UnAuthorized(exception.Message),
            UnAuthenticationException => Result.UnAuthentication(exception.Message),
            FluentValidation.ValidationException => GetValidationError(((FluentValidation.ValidationException)exception).Errors),
            _ => Result.Failure("Server errors", exception.Message),
        };

        var result = JsonSerializer.Serialize(data);
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
            UnAuthenticationException => StatusCodes.Status401Unauthorized,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    public static object GetValidationError(IEnumerable<FluentValidation.Results.ValidationFailure> errors)
    {
        List<ValidationErorr> validationErorrs = new List<ValidationErorr>();
        foreach (var error in errors)
        {
            validationErorrs.Add(new ValidationErorr(error.PropertyName, error.ErrorMessage));
        }
        return Result.ValidationError(validationErorrs);
    }
}
