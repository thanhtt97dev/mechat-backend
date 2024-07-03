using AutoMapper;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;
using System.Threading.Tasks;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class AddUserCommandHandler : ICommandHandler<Command.AddUser>
{
    private readonly IRepositoryEnitityBase<Domain.Entities.User, Guid> userRepository;
    private readonly IMapper mapper;

    public AddUserCommandHandler(IRepositoryEnitityBase<Domain.Entities.User, Guid> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public  Task<Result> Handle(Command.AddUser request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Domain.Entities.User>(request);
        user.Avatar = null;
        user.Email = null;
        user.DateCreated = DateTime.Now;
        user.DateUpdated = DateTime.Now;
        user.Status = UserConstant.Status.Activate;
        user.RoldeId = RoleConstant.User;

        userRepository.Add(user);
        return Task.FromResult<Result>(Result.Success());
    }
}
