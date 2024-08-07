using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Storage;

namespace MeChat.Application.UseCases.V1.Storage.CommandHandlers;
public class UploadFileCommandHandler : ICommandHandler<Command.UploadFile>
{
    private readonly IStorageService storageService;

    public UploadFileCommandHandler(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<Result> Handle(Command.UploadFile request, CancellationToken cancellationToken)
    {
        await storageService.UploadFileAsync(request.File, request.FileName);

        return Result.Success();
    }
}
