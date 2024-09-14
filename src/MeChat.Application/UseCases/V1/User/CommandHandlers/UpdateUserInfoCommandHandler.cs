using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class UpdateUserInfoCommandHandler : ICommandHandler<Command.UpdateUserInfo>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF;
    private readonly IStorageService storageService;

    public UpdateUserInfoCommandHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository, 
        IStorageService storageService,
        Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF)
    {
        this.userRepository = userRepository;
        this.storageService = storageService;
        this.unitOfWorkEF = unitOfWorkEF;
    }

    public async Task<Result> Handle(Command.UpdateUserInfo request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindSingleAsync(x => x.Id == request.Id);
        if (user == null)
            return Result.NotFound("User not found");
        if(user.Status != AppConstants.User.Status.Activate)
            return Result.UnAuthorized("User has been baned!");

        //update
        user.Fullname = request.Fullname;
        if (request.Avatar != null)
        {
            //remove old image
            if (!string.IsNullOrEmpty(user.Avatar))
                await storageService.DeleteFileAsync(user.Avatar!.Substring(user.Avatar.LastIndexOf("/") + 1));

            //upload new image
            DateTime now  = DateTime.Now;
            var fileName = string.Format("{0}_{1}{2}{3}{4}{5}{6}{7}{8}", request.Id.ToString(), now.Year, now.Month,
                    now.Day, now.Millisecond, now.Second, now.Minute, now.Hour,
                    request.Avatar.FileName.Substring(request.Avatar.FileName.LastIndexOf(".")));
            var url = await storageService.UploadFileAsync(request.Avatar, fileName);
            user.Avatar = url;
        }
        await unitOfWorkEF.SaveChangeAsync();
        return Result.Success();
    }
}
