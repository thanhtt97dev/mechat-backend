using MeChat.Common.Shared.Response;
using MediatR;

namespace MeChat.Common.Abstractions.Messages.DomainEvents;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
