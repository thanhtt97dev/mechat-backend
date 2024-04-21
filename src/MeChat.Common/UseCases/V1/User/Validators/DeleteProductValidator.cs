using FluentValidation;

namespace MeChat.Common.UseCases.V1.User.Validators;
public class DeleteProductValidator : AbstractValidator<Command.DeleteUser>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
