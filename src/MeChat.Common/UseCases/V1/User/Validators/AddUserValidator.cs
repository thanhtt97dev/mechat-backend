using FluentValidation;

namespace MeChat.Common.UseCases.V1.User.Validators;
public class AddUserValidator : AbstractValidator<Command.AddUser>
{
    public AddUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
