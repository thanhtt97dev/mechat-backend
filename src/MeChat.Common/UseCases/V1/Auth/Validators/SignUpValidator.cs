using FluentValidation;

namespace MeChat.Common.UseCases.V1.Auth.Validators;
public class SignUpValidator : AbstractValidator<Command.SignUp>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(8).MaximumLength(15);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(15);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
