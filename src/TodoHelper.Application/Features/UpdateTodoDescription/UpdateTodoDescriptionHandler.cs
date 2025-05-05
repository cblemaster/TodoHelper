
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoDescription;

internal sealed class UpdateTodoDescriptionHandler(ITodosRepository repository) : HandlerBase<UpdateTodoDescriptionCommand, UpdateTodoDescriptionResponse>(repository)
{
    public override Task<Result<UpdateTodoDescriptionResponse>> HandleAsync(UpdateTodoDescriptionCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoDescriptionResponse>.NotFoundFailure($"Todo with id {command.TodoId} not found."));
        }

        Result<Todo> todoResult = todo.SetDescription(command.Description);

        // Rule: Todo description must not be null, an empty string, nor all-whitespace characters
        // Rule: Todo description must be 255 characters or fewer
        if (todoResult.IsFailure && todoResult.Error is string error)
        {
            return Task.FromResult(Result<UpdateTodoDescriptionResponse>.ValidationFailure(error));
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Task.FromResult(Result<UpdateTodoDescriptionResponse>.DomainRuleFailure($"Completed todos cannot be updated."));
        }
        else if (todoResult.IsSuccess && todoResult.Value is Todo updatedTodo)
        {
            _ = _repository.UpdateTodoAsync(updatedTodo);
            return Task.FromResult(Result<UpdateTodoDescriptionResponse>.Success(new UpdateTodoDescriptionResponse(true)));
        }
        else
        {
            return Task.FromResult(Result<UpdateTodoDescriptionResponse>.UnknownFailure("An unknown error occurred when updating the category."));
        }
    }
}
