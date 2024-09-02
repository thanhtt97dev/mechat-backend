using FluentValidation;

namespace MeChat.Common.UseCases.V1.Auth.Validators;
public class SignUpValidator : AbstractValidator<Command.SignUp>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
