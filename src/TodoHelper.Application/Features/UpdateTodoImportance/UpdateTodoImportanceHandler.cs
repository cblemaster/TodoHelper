
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoImportance;

internal sealed class UpdateTodoImportanceHandler(ITodosRepository repository) : HandlerBase<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse>(repository)
{
    public override Task<Result<UpdateTodoImportanceResponse>> HandleAsync(UpdateTodoImportanceCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoImportanceResponse>.NotFoundFailure($"Todo with id {command.TodoId} not found."));
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Task.FromResult(Result<UpdateTodoImportanceResponse>.DomainRuleFailure($"Completed todos cannot be updated."));
        }
        else
        {
            _ = _repository.UpdateTodoImportanceAsync(todo);
            return Task.FromResult(Result<UpdateTodoImportanceResponse>.Success(new UpdateTodoImportanceResponse(true)));
        }
    }
}
