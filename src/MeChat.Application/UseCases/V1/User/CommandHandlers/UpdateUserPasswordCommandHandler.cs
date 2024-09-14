using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class UpdateUserPasswordCommandHandler : ICommandHandler<Command.UpdateUserPassword>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF;

    public UpdateUserPasswordCommandHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository,
        IUnitOfWork unitOfWorkEF)
    {
        this.userRepository = userRepository;
        this.unitOfWorkEF = unitOfWorkEF;
    }

    public async Task<Result> Handle(Command.UpdateUserPassword request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindSingleAsync(x => x.Id == request.Id);
        if (user == null)
            return Result.NotFound("User not found");
        if (user.Status != AppConstants.User.Status.Activate)
            return Result.UnAuthorized("User has been baned!");

        //check old password
        if (!string.IsNullOrEmpty(user.Username) && user.Password != request.OldPassword)
            return Result.Initialization(AppConstants.ResponseCodes.User.WrongPassword, "Invalid passowrd", false);

        //check usernaem existed
        if (string.IsNullOrEmpty(user.Username))
        {
            var isUsernameExisted = await userRepository.Any(x => x.Username == request.Username);
            if (isUsernameExisted)
                return Result.Initialization(AppConstants.ResponseCodes.User.UsernameExisted, "Username existed", false);
        }

        user.Username = request.Username;
        user.Password = request.NewPassword;

        await unitOfWorkEF.SaveChangeAsync();

        return Result.Success();
    }
}
