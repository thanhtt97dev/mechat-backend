using MeChat.Common.Shared.Response;
using MediatR;

namespace MeChat.Common.Abstractions.Messages;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
