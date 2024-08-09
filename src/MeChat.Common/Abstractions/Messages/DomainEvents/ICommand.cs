using MeChat.Common.Abstractions.Messages.DomainEvents.Annotations;
using MeChat.Common.Shared.Response;
using MediatR;

namespace MeChat.Common.Abstractions.Messages.DomainEvents;
public interface ICommand : IRequest<Result>, IDbTransactionAnnotation { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IDbTransactionAnnotation { }
