
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
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
            dtos.Add(t.MapToDTO()));
        // Specification: Sorted by is complete descending, then by due date descending, then by description
        _ = dtos.OrderByDescending(d => d.DueDate).ThenBy(d => d.Description);
        GetTodosForCategoryResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosForCategoryResponse>.Success(response));
    }
}
