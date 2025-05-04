
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal sealed class GetTodosCompletedHandler(ITodosRepository repository) : HandlerBase<GetTodosCompletedCommand, GetTodosCompletedResponse>(repository)
{
    public override Task<Result<GetTodosCompletedResponse>> HandleAsync(GetTodosCompletedCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Todo> todos =
            base._repository.GetTodos()
                .Where(t => t.IsComplete)
                .OrderByDescending(command.FirstOrderByPredicate.Compile())
                .ThenBy(command.SecondOrderByPredicate.Compile());

        GetTodosCompletedResponse response = new(todos);
        return Task.FromResult(Result<GetTodosCompletedResponse>.Success(response));
    }
}
