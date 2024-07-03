using MeChat.Common.Abstractions.Middlewares;
using MeChat.Common.Shared.Response;
using MediatR;

namespace MeChat.Common.Abstractions.Messages;
public interface ICommand : IRequest<Result>, IDbTransactionMiddleware { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IDbTransactionMiddleware { }
