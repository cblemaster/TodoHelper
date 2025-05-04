
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoDueDate;

internal sealed class UpdateTodoDueDateHandler(ITodosRepository repository) : HandlerBase<UpdateTodoDueDateCommand, UpdateTodoDueDateResponse>(repository)
{
    public override Task<Result<UpdateTodoDueDateResponse>> HandleAsync(UpdateTodoDueDateCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodos().Single(t => t.Id.Value == command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoDueDateResponse>.Failure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            _ = _repository.UpdateTodoDueDateAsync(todo, command.DueDate);
            return Task.FromResult(Result<UpdateTodoDueDateResponse>.Success(new UpdateTodoDueDateResponse(true)));
        }
    }
}
