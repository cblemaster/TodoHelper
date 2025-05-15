
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Common;

internal abstract class HandlerBase<T, TCommand, TResponse>(IRepository<T> repository) : ICommandHandler<TCommand, TResponse> where T :Entity<T>
{
    protected readonly IRepository<T> _repository = repository;

    public abstract Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
