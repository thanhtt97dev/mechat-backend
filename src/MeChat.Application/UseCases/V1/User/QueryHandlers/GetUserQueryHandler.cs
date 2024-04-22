using AutoMapper;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Shared.Exceptions;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.QueryHandlers;
public class GetUserQueryHandler : IQueryHandler<Query.GetUserById, Response.User>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<Response.User>> Handle(Query.GetUserById request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindByIdAsync(request.Id) ?? throw new UserExceptions.NotFound(request.Id);
        var result = mapper.Map<Response.User>(user);
        return Result.Success(result);
    }
}
