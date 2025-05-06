
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
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
        todos.ForEach(t => dtos.Add(t.MapToDTO()));

        // Specification: Sorted by due date descending, then by description
        _ = dtos.OrderByDescending(d => d.DueDate).ThenBy(d => d.Description);
        GetTodosImportantResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosImportantResponse>.Success(response));
    }
}
