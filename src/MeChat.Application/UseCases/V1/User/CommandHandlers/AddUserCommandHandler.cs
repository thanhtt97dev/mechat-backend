using AutoMapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;
using MeChat.Domain.Abstractions.EntityFramework;
using MeChat.Domain.Abstractions.EntityFramework.Repositories;
using System.Threading.Tasks;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class AddUserCommandHandler : ICommandHandler<Command.AddUser>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IMapper mapper;

    public AddUserCommandHandler(IRepositoryBase<Domain.Entities.User, Guid> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public  Task<Result> Handle(Command.AddUser request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Domain.Entities.User>(request);

        userRepository.Add(user);
        return Task.FromResult<Result>(Result.Success());
    }
}
