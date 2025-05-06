
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoDescription;

internal sealed class UpdateTodoDescriptionHandler(ITodosRepository repository) : HandlerBase<UpdateTodoDescriptionCommand, UpdateTodoDescriptionResponse>(repository)
{
    public async override Task<Result<UpdateTodoDescriptionResponse>> HandleAsync(UpdateTodoDescriptionCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Result<UpdateTodoDescriptionResponse>.NotFoundFailure(ApplicationErrors.NotFoundErrorMessage(nameof(Todo), command.TodoId));
        }

        Result<Todo> todoResult = todo.SetDescription(command.Description);

        // Rule: Todo description must not be null, an empty string, nor all-whitespace characters
        // Rule: Todo description must be 255 characters or fewer
        if (todoResult.IsFailure && todoResult.Error is string error)
        {
            return Result<UpdateTodoDescriptionResponse>.ValidationFailure(error);
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Result<UpdateTodoDescriptionResponse>.DomainRuleFailure(DomainErrors.CannotUpdateCompletedTodosErrorMessage());
        }
        else if (todoResult.IsSuccess && todoResult.Value is Todo updatedTodo)
        {
            await _repository.UpdateTodoAsync(updatedTodo);
            return Result<UpdateTodoDescriptionResponse>.Success(new UpdateTodoDescriptionResponse(true));
        }
        else
        {
            return Result<UpdateTodoDescriptionResponse>.UnknownFailure(DomainErrors.UnknownErrorMessage("updating the todo"));
        }
    }
}
