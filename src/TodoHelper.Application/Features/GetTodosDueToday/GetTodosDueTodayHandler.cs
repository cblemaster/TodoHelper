
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal sealed class GetTodosDueTodayHandler(ITodosRepository repository) : HandlerBase<GetTodosDueTodayCommand, GetTodosDueTodayResponse>(repository)
{
    public override Task<Result<GetTodosDueTodayResponse>> HandleAsync(GetTodosDueTodayCommand command, CancellationToken cancellationToken = default)
    {
        List<TodoDTO> dtos = [];
        List<Todo> todos = [.. _repository.GetTodos().Where(command.WherePredicate())];
        todos.ForEach(t => dtos.Add(t.MapToDTO()));

        // SPECIFICATION: Sorted by description
        _ = dtos.OrderBy(command.SortByDescriptionPredicate());
        GetTodosDueTodayResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosDueTodayResponse>.Success(response));
    }
}
