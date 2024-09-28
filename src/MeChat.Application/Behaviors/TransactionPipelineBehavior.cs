using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Messages.DomainEvents.Annotations;
using MediatR;

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

        await unitOfWork.BeginTransactionAsync();
        try
        {
            var response = await next();
            await unitOfWork.SaveChangeAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync();
            return response;
        }
        catch(Exception exception) 
        {
            await unitOfWork.RollbackTransactionAsync();
#pragma warning disable CA2200 // Rethrow to preserve stack details
            throw exception;
#pragma warning restore CA2200 // Rethrow to preserve stack details
        }
        finally
        {
            await unitOfWork.DisposeAsync();
        }
        
    }

    private static bool IsCommand()
    {
        var isRequestNeedDbTranasction = typeof(TRequest).GetInterfaces().FirstOrDefault(x => x.Name == nameof(IDbTransactionAnnotation)) != null;
        return isRequestNeedDbTranasction;

    }
}
