
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
        List<Todo> todos = [.. _repository.GetTodos().Where(t => t.DueDate.Value is not null && t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today))];
        todos.ForEach(t =>
            dtos.Add(t.MapToDTO()));
        // Specification: Sorted by description
        _ = dtos.OrderBy(d => d.Description);
        GetTodosDueTodayResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosDueTodayResponse>.Success(response));
    }
}
