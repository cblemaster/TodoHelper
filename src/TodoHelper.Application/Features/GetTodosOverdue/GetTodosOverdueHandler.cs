
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal sealed class GetTodosOverdueHandler(ITodosRepository repository) : HandlerBase<GetTodosOverdueCommand, GetTodosOverdueResponse>(repository)
{
    public override Task<Result<GetTodosOverdueResponse>> HandleAsync(GetTodosOverdueCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Todo> todos =
            _repository.GetTodos()
                .Where(t => t.DueDate.Value is not null && t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today) && !t.IsComplete)
                .OrderByDescending(command.FirstOrderByPredicate.Compile())
                .ThenBy(command.SecondOrderByPredicate.Compile());
        GetTodosOverdueResponse response = new(todos);
        return Task.FromResult(Result<GetTodosOverdueResponse>.Success(response));
    }
}
