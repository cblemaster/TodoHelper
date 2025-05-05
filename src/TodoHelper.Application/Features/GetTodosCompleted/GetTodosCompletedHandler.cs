
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal sealed class GetTodosCompletedHandler(ITodosRepository repository) : HandlerBase<GetTodosCompletedCommand, GetTodosCompletedResponse>(repository)
{
    public override Task<Result<GetTodosCompletedResponse>> HandleAsync(GetTodosCompletedCommand command, CancellationToken cancellationToken = default)
    {
        List<TodoDTO> dtos = [];
        List<Todo> todos = [.. _repository.GetTodos().Where(t => t.CompleteDate.Value is not null)];
        todos.ForEach(t => dtos.Add(new TodoDTO(t.Id.Value, t.Category.Name.Value, t.CategoryId.Value, t.Description.Value, t.DueDate.Value, t.CompleteDate.Value, t.CreateDate.Value, t.UpdateDate.Value, t.Importance.IsImportant)));
        _ = dtos.OrderBy(command.FirstOrderByPredicate.Compile()).ThenBy(command.SecondOrderByPredicate.Compile());
        GetTodosCompletedResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosCompletedResponse>.Success(response));
    }
}
