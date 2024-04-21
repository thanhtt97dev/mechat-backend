﻿using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Shared.Exceptions;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;
using MeChat.Domain.Abstractions.EntityFramework.Repositories;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class UpdateUserCommandHandler : ICommandHandler<Command.UpdateUser>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    public UpdateUserCommandHandler(IRepositoryBase<Domain.Entities.User, Guid> userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result> Handle(Command.UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.Id) ?? throw new UserExceptions.UserNotFound(request.Id);

        user.Username = request.Username;
        user.Password = request.Password;

        return Result.Success();
    }
}
