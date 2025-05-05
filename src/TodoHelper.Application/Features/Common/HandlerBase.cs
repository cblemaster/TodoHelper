
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Common;

public abstract class HandlerBase<TCommand, TResponse>(ITodosRepository repository) : ICommandHandler<TCommand, TResponse>
{
    protected readonly ITodosRepository _repository = repository;

    public abstract Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
