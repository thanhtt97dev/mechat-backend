using FluentValidation;

namespace MeChat.Common.UseCases.V1.User.Validators;
public class UpdateUserValidator : AbstractValidator<Command.UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
