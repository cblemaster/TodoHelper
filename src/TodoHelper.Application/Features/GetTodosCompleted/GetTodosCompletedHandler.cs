
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
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
        List<Todo> todos = [.. _repository.GetTodos().Where(command.WherePredicate())];
        todos.ForEach(t => dtos.Add(t.MapToDTO()));

        // SPECIFICATION: Sort by due date descending, then by description
        _ = dtos.OrderByDescending(command.SortByDueDatePredicate()).ThenBy(command.SortByDescriptionPredicate());
        GetTodosCompletedResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosCompletedResponse>.Success(response));
    }
}
