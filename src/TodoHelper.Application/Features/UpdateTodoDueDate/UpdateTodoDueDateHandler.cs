
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoDueDate;

internal sealed class UpdateTodoDueDateHandler(ITodosRepository repository) : HandlerBase<UpdateTodoDueDateCommand, UpdateTodoDueDateResponse>(repository)
{
    public async override Task<Result<UpdateTodoDueDateResponse>> HandleAsync(UpdateTodoDueDateCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Result<UpdateTodoDueDateResponse>.NotFoundFailure(ApplicationErrors.NotFoundErrorMessage(nameof(Todo), command.TodoId));
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Result<UpdateTodoDueDateResponse>.DomainRuleFailure(DomainErrors.CannotUpdateCompletedTodosErrorMessage());
        }
        else
        {
            await _repository.UpdateTodoDueDateAsync(todo, command.DueDate);
            return Result<UpdateTodoDueDateResponse>.Success(new UpdateTodoDueDateResponse(true));
        }
    }
}
