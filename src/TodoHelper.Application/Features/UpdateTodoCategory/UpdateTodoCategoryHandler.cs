
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Features.UpdateTodoDueDate;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Application.Features.UpdateTodoCategory;

internal sealed class UpdateTodoCategoryHandler(ITodosRepository repository) : HandlerBase<UpdateTodoCategoryCommand, UpdateTodoCategoryResponse>(repository)
{
    public override Task<Result<UpdateTodoCategoryResponse>> HandleAsync(UpdateTodoCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoCategoryResponse>.NotFoundFailure($"Todo with id {command.TodoId} not found."));
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Task.FromResult(Result<UpdateTodoCategoryResponse>.DomainRuleFailure($"Completed todos cannot be updated."));
        }
        // Rule: Todo must have a category
        else if (todo.CategoryId is null)
        {
            return Task.FromResult(Result<UpdateTodoCategoryResponse>.DomainRuleFailure($"Todos must belong to a category."));
        }
        else
        {
            _ = _repository.UpdateTodoCategoryAsync(todo, Identifier<Category>.Create(command.CategoryId));
            return Task.FromResult(Result<UpdateTodoCategoryResponse>.Success(new UpdateTodoCategoryResponse(true)));
        }
    }
}
