using FluentValidation;

namespace MeChat.Common.UseCases.V1.User.Validators;
public class UpdateUserPasswordValidator: AbstractValidator<Command.UpdateUserPassword>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
    }
}
