using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using Microsoft.Extensions.Configuration;

namespace MeChat.Application.UseCases.V1.Auth.CommandHandlers;
public class SignUpCommandHandler : ICommandHandler<Command.SignUp>
{
    private readonly IConfiguration configuration;
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userReposiory;
    private readonly IUnitOfWork unitOfWorkEF;
    private readonly IMessageBrokerProducerEmail messageBrokerProducerEmail;

    private readonly AuthUtil authUtil;

    public SignUpCommandHandler
        (IConfiguration configuration,
        IRepositoryBase<Domain.Entities.User, Guid> userReposiory,
        IUnitOfWork unitOfWorkEF,
        IMessageBrokerProducerEmail messageBrokerProducerEmail,
        AuthUtil authUtil)
    {
        this.configuration = configuration;
        this.userReposiory = userReposiory;
        this.unitOfWorkEF = unitOfWorkEF;
        this.authUtil = authUtil;
        this.messageBrokerProducerEmail = messageBrokerProducerEmail;

    }

    public async Task<Result> Handle(Command.SignUp request, CancellationToken cancellationToken)
    {
        var isEmailExisted = await userReposiory.Any(x => x.Email == request.Email);
        if (isEmailExisted)
            return Result.Failure("Email has been used in orthor account!");

        var isUsernameExisted = await userReposiory.Any(x => x.Username == request.Username);
        if(isUsernameExisted)
            return Result.Failure("Username has been used in orthor account!");

        Domain.Entities.User user = new Domain.Entities.User()
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Password = request.Password,
            RoleId = AppConstants.Role.User,
            Email = request.Email,
            Fullname = request.Username,
            Status = AppConstants.User.Status.UnActivate
        };

        userReposiory.Add(user);
        await unitOfWorkEF.SaveChangeUserTrackingAsync(user.Id);

        //send mail
        string subject = "MeChat - Confirm Sign up account";
        string enpoint = $"{configuration["FrontEnd:Endpoint"] ?? string.Empty}/confirmSignUp";
        string accessToken = authUtil.GenerateTokenForSignUp(request.Email);
        string content =
$@"
<div>
    <p>Please click to link below to confirm!</p><br/>
    <a href='{enpoint}?accessToken={accessToken}'>click here</a>
</div>";

        await messageBrokerProducerEmail.SendMailAsync(request.Email, subject, content);

        return Result.Success();
    }
}
