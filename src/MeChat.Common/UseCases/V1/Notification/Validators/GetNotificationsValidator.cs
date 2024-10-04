using FluentValidation;

namespace MeChat.Common.UseCases.V1.Notification.Validators;
public class GetNotificationsValidator : AbstractValidator<Query.GetNotifications>
{
    public GetNotificationsValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PageIndex).GreaterThan(0);
    }
}
