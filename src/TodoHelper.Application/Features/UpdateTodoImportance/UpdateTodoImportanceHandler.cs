
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoImportance;

internal sealed class UpdateTodoImportanceHandler(ITodosRepository repository) : HandlerBase<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse>(repository)
{
    public async override Task<Result<UpdateTodoImportanceResponse>> HandleAsync(UpdateTodoImportanceCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Result<UpdateTodoImportanceResponse>.NotFoundFailure(DomainErrors.NotFoundErrorMessage(nameof(Todo), command.TodoId));
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Result<UpdateTodoImportanceResponse>.DomainRuleFailure(DomainErrors.CannotUpdateCompletedTodosErrorMessage());
        }
        else
        {
            await _repository.UpdateTodoImportanceAsync(todo);
            return Result<UpdateTodoImportanceResponse>.Success(new UpdateTodoImportanceResponse(true));
        }
    }
}
