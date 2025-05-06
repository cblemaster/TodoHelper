
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
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
        List<Todo> todos = [.. _repository.GetTodos().Where(command.WherePredicate())];
        todos.ForEach(t => dtos.Add(t.MapToDTO()));

        // Specification: Sorted by due date descending, then by description 
        _ = dtos.OrderByDescending(command.SortByDueDatePredicate()).ThenBy(command.SortByDescriptionPredicate());
        GetTodosOverdueResponse response = new(dtos);
        return Task.FromResult(Result<GetTodosOverdueResponse>.Success(response));
    }
}
