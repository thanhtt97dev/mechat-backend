using AutoMapper;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.QueryHandlers;
public class GetUsersQueryHandler : IQueryHandler<Query.GetUsers, PageResult<Response.User>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<PageResult<Response.User>>> Handle(Query.GetUsers request, CancellationToken cancellationToken)
    {
        var totalRecord = await unitOfWork.Users.GetTotalRecord();
        var users = await unitOfWork.Users.GetManyAsync(request.SearchTerm, request.SortColumnWithOrders, request.PageIndex, request.PageSize);

        var pageResult = PageResult<Domain.Entities.User>
            .Create(users, request.PageIndex, request.PageSize, totalRecord);
        
        var result = mapper.Map<PageResult<Response.User>>(pageResult);

        return Result.Success(result);
    }
}
