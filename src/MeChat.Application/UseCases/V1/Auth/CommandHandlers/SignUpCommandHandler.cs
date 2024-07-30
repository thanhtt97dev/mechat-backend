using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages;
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
    private readonly IMailService mailService;

    private readonly AuthUtil authUtil;

    public SignUpCommandHandler(IConfiguration configuration, IRepositoryBase<Domain.Entities.User, Guid> userReposiory, IMailService mailService, AuthUtil authUtil)
    {
        this.configuration = configuration;
        this.userReposiory = userReposiory;
        this.mailService = mailService;
        this.authUtil = authUtil;
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
            Username = request.Username,
            Password = request.Password,
            RoldeId = AppConstants.Role.User,
            Email = request.Email,
            Fullname = request.Username,
            Status = AppConstants.User.Status.UnActivate
        };

        userReposiory.Add(user);

        //send mail
        string subject = "MeChat - Confirm Sign up account";
        string enpoint = configuration["Endpoint"]??string.Empty;
        string accessToken = authUtil.GenerateTokenForSignUp(request.Email);
        string content =
$@"<p>Please click to link below to confirm!</p><br/>
<a href='{enpoint}&accessToken={accessToken}'>click hear</a>";
        await mailService.SendMailAsync(request.Email, subject, content);

        return Result.Success();
    }
}
