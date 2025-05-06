
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.Specifications;

namespace TodoHelper.Application.Features.UpdateTodoImportance;

internal sealed class UpdateTodoImportanceHandler(ITodosRepository repository) : HandlerBase<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse>(repository)
{
    public async override Task<Result<UpdateTodoImportanceResponse>> HandleAsync(UpdateTodoImportanceCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Result<UpdateTodoImportanceResponse>.NotFoundFailure(ApplicationErrors.NotFoundErrorMessage(nameof(Todo), command.TodoId));
        }
        // RULE: Complete todos cannot be updated, except to update to not complete
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
