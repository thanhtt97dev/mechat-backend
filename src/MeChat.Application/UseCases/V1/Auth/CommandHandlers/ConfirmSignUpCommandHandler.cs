using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Persistence.Repositories;

namespace MeChat.Application.UseCases.V1.Auth.CommandHandlers;
public class ConfirmSignUpCommandHandler : ICommandHandler<Command.ConfirmSignUp>
{
    private readonly IUnitOfWork unitOfWorkEF;
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    private readonly IJwtService jwtTokenService;

    public ConfirmSignUpCommandHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository,
        IUnitOfWork unitOfWorkEF,
        IJwtService jwtTokenService)
    {
        this.unitOfWorkEF = unitOfWorkEF;
        this.userRepository = userRepository;
        this.jwtTokenService = jwtTokenService;
    }

    public async Task<Result> Handle(Command.ConfirmSignUp request, CancellationToken cancellationToken)
    {
        bool isValidAccessToken = jwtTokenService.ValidateAccessToken(request.AccessToken, false);
        if (isValidAccessToken is false)
            return Result.UnAuthentication("Invalid access token");

        var emailSignUp = jwtTokenService.GetClaim(AppConstants.AppConfigs.Jwt.EMAIL, request.AccessToken, false)?.ToString();
        if (emailSignUp == null)
            return Result.UnAuthentication("UnAuthentication");

        var user = await userRepository.FindSingleAsync(user => user.Email.Equals(emailSignUp));
        if(user == null)
            return Result.NotFound("Account is not registed");

        if (user.Status != AppConstants.User.Status.UnActivate)
            return Result.Success();

        user.Status = AppConstants.User.Status.Activate;
        userRepository.Update(user);
        await unitOfWorkEF.SaveChangeUserTrackingAsync(user.Id);

        return Result.Success();
    }
}
