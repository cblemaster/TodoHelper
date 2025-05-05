
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed class GetTodosForCategoryHandler(ITodosRepository repository) : HandlerBase<GetTodosForCategoryCommand, GetTodosForCategoryResponse>(repository)
{
    public override Task<Result<GetTodosForCategoryResponse>> HandleAsync(GetTodosForCategoryCommand command, CancellationToken cancellationToken = default)
    {
        List<TodoDTO> dtos = [];
        List<Todo> todos = [.. _repository.GetTodos().Where(t => t.CategoryId.Value == command.CategoryId)];
        todos.ForEach(t =>
            dtos.Add(new TodoDTO(t.Id.Value, t.Category.Name.Value, t.CategoryId.Value, t.Description.Value, t.DueDate.Value,
                t.CompleteDate.Value, t.CreateDate.Value, t.UpdateDate.Value, t.Importance.IsImportant)));
        _ = dtos.OrderBy(command.FirstOrderByPredicate.Compile()).ThenBy(command.SecondOrderByPredicate.Compile());
        GetTodosForCategoryResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosForCategoryResponse>.Success(response));
    }
}
