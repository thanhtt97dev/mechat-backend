using AutoMapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;
using MeChat.Domain.Abstractions.EntityFramework;
using MeChat.Domain.Abstractions.EntityFramework.Repositories;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class AddUserCommandHandler : ICommandHandler<Command.AddUser>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AddUserCommandHandler(IRepositoryBase<Domain.Entities.User, Guid> userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result> Handle(Command.AddUser request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Domain.Entities.User>(request);

        userRepository.Add(user);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
