
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal sealed class GetTodosImportantHandler(ITodosRepository repository) : HandlerBase<GetTodosImportantCommand, GetTodosImportantResponse>(repository)
{
    public override Task<Result<GetTodosImportantResponse>> HandleAsync(GetTodosImportantCommand command, CancellationToken cancellationToken = default)
    {
        List<TodoDTO> dtos = [];
        List<Todo> todos = [.. _repository.GetTodos().Where(t => t.Importance.IsImportant)];
        todos.ForEach(t =>
            dtos.Add(new TodoDTO(t.Id.Value, t.Category.Name.Value, t.CategoryId.Value, t.Description.Value, t.DueDate.Value,
                t.CompleteDate.Value, t.CreateDate.Value, t.UpdateDate.Value, t.Importance.IsImportant)));
        _ = dtos.OrderByDescending(command.FirstOrderByPredicate.Compile()).ThenBy(command.SecondOrderByPredicate.Compile());
        GetTodosImportantResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosImportantResponse>.Success(response));
    }
}
