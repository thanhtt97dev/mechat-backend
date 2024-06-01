using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Storage;

namespace MeChat.Application.UseCases.V1.Storage.CommandHandlers;
public class DeleteFileCommandHandler : ICommandHandler<Command.DeleteFile>
{
    private readonly IStorageService storageService;

    public DeleteFileCommandHandler(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<Result> Handle(Command.DeleteFile request, CancellationToken cancellationToken)
    {
        await storageService.DeleteFileAsync(request.fileName);
        return Result.Success();
    }
}
