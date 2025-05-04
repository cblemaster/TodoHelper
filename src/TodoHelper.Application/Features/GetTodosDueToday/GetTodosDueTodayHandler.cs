
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal sealed class GetTodosDueTodayHandler(ITodosRepository repository) : HandlerBase<GetTodosDueTodayCommand, GetTodosDueTodayResponse>(repository)
{
    public override Task<Result<GetTodosDueTodayResponse>> HandleAsync(GetTodosDueTodayCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Todo> todos =
            _repository.GetTodos()
                .Where(t => t.DueDate.Value is not null && t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today) && !t.IsComplete)
                .OrderBy(command.OrderByPredicate.Compile());
        GetTodosDueTodayResponse response = new(todos);
        return Task.FromResult(Result<GetTodosDueTodayResponse>.Success(response));
    }
}
