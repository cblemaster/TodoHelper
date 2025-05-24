
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.Application.Features.Common;

internal abstract class HandlerBase<T, TCommand, TResponse>(IRepository<T> repository) : ICommandHandler<TCommand, TResponse> where T : Entity<T>
{
    protected readonly IRepository<T> _repository = repository;

    public abstract Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
