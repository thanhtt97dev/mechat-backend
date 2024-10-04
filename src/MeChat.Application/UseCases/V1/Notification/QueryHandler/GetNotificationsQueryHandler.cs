using AutoMapper;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Notification;

namespace MeChat.Application.UseCases.V1.Notification.QueryHandler;
public class GetNotificationsQueryHandler : IQueryHandler<Query.GetNotifications, PageResult<Response.Notification>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetNotificationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<PageResult<Response.Notification>>> Handle(Query.GetNotifications request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var notifications = await unitOfWork.Notifications.GetManyAsync(id, request.PageIndex, AppConstants.Page.Size5Record);
        var total = await unitOfWork.Notifications.GetTotalRecord(id);

        var notificatonResult = mapper.Map<List<Response.Notification>>(notifications);

        var result = PageResult<Response.Notification>.Create(notificatonResult, request.PageIndex, AppConstants.Page.Size5Record, total);

        return Result.Success(result);
    }
}
