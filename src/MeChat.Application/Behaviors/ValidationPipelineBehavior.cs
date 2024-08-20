using FluentValidation;
using FluentValidation.Results;
using MeChat.Common.Shared.Response;
using MediatR;

namespace MeChat.Application.Behaviors;
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        IEnumerable<ValidationFailure> validationErrorDetails = validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new ValidationFailure() { PropertyName = failure.PropertyName, ErrorMessage = failure.ErrorMessage })
            .DistinctBy(x => x.PropertyName)
            .ToArray();
    
        if (validationErrorDetails.Count() > 0)
        {
            throw new FluentValidation.ValidationException("Validation errors", validationErrorDetails);
        }

        return await next();
    }
}
