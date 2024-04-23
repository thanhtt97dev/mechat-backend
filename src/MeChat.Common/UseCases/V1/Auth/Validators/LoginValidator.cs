using FluentValidation;

namespace MeChat.Common.UseCases.V1.Auth.Validators;
public class LoginValidator : AbstractValidator<Query.Login>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
