using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Middlewares;
using MediatR;
using System.Transactions;

namespace MeChat.Application.Behaviors;
public sealed class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUnitOfWork unitOfWork;

    public TransactionPipelineBehavior(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand())
            return await next();

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var response = await next();
        await unitOfWork.SaveChangeAsync(cancellationToken);
        transaction.Complete();
        await unitOfWork.DisposeAsync();
        return response;
    }

    private static bool IsCommand()
    {
        var isRequestNeedDbTranasction = typeof(TRequest).GetInterfaces().FirstOrDefault(x => x.Name == nameof(IDbTransactionMiddleware)) != null;
        return isRequestNeedDbTranasction;

    }
}
