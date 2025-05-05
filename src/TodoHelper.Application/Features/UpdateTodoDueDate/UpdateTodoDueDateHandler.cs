
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoDueDate;

internal sealed class UpdateTodoDueDateHandler(ITodosRepository repository) : HandlerBase<UpdateTodoDueDateCommand, UpdateTodoDueDateResponse>(repository)
{
    public override Task<Result<UpdateTodoDueDateResponse>> HandleAsync(UpdateTodoDueDateCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoDueDateResponse>.NotFoundFailure($"Todo with id {command.TodoId} not found."));
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Task.FromResult(Result<UpdateTodoDueDateResponse>.DomainRuleFailure($"Completed todos cannot be updated."));
        }
        else
        {
            _ = _repository.UpdateTodoDueDateAsync(todo, command.DueDate);
            return Task.FromResult(Result<UpdateTodoDueDateResponse>.Success(new UpdateTodoDueDateResponse(true)));
        }
    }
}
