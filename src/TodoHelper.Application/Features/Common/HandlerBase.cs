
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Common;

internal abstract class HandlerBase<TCommand, TResponse>(ITodosRepository<_Category> repository) : ICommandHandler<TCommand, TResponse>
{
    protected readonly ITodosRepository<_Category> _repository = repository;

    public abstract Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
