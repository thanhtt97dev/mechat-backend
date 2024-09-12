using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Shared.Enumerations;
using MeChat.Common.Shared.Response;

namespace MeChat.Common.UseCases.V1.User;
public class Query
{
    public record GetUserById(Guid Id, string AccessToken) : IQuery<Response.User>;
    public record GetUsers(string? SearchTerm, IDictionary<string, SortOrderSql> SortColumnWithOrders, int PageIndex, int PageSize) : IQuery<PageResult<Response.User>>;
    public record GetUserPublicInfo(string Key, string? Id) : IQuery<Response.UserPublicInfo>;
}
