using FluentValidation;

namespace MeChat.Common.UseCases.V1.User.Validators;
public class GetUsecrPublicInfoValidator : AbstractValidator<Query.GetUserPublicInfo>
{
    public GetUsecrPublicInfoValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
    }
}
