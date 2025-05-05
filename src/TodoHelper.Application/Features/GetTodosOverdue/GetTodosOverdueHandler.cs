
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal sealed class GetTodosOverdueHandler(ITodosRepository repository) : HandlerBase<GetTodosOverdueCommand, GetTodosOverdueResponse>(repository)
{
    public override Task<Result<GetTodosOverdueResponse>> HandleAsync(GetTodosOverdueCommand command, CancellationToken cancellationToken = default)
    {
        List<TodoDTO> dtos = [];
        List<Todo> todos = [.. _repository.GetTodos().Where(t => t.DueDate.Value is not null && t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today))];
        todos.ForEach(t => dtos.Add(new TodoDTO(t.Id.Value, t.Category.Name.Value, t.CategoryId.Value, t.Description.Value, t.DueDate.Value, t.CompleteDate.Value, t.CreateDate.Value, t.UpdateDate.Value, t.Importance.IsImportant)));
        _ = dtos.OrderByDescending(command.FirstOrderByPredicate.Compile()).ThenBy(command.SecondOrderByPredicate.Compile());
        GetTodosOverdueResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosOverdueResponse>.Success(response));
    }
}
