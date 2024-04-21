using MeChat.Common.Shared.Response;
using MediatR;

namespace MeChat.Common.Abstractions.Messages;
public interface ICommand : IRequest<Result> { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
