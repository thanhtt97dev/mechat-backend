using FluentValidation;

namespace MeChat.Common.UseCases.V1.Auth.Validators;
public class RefreshTokenValidator : AbstractValidator<Query.RefreshToken>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.Refresh).NotEmpty();
    }
}
