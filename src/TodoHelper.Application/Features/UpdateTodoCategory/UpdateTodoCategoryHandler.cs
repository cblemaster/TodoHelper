
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Application.Features.UpdateTodoCategory;

internal sealed class UpdateTodoCategoryHandler(ITodosRepository repository) : HandlerBase<UpdateTodoCategoryCommand, UpdateTodoCategoryResponse>(repository)
{
    public async override Task<Result<UpdateTodoCategoryResponse>> HandleAsync(UpdateTodoCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Result<UpdateTodoCategoryResponse>.NotFoundFailure(ApplicationErrors.NotFoundErrorMessage(nameof(Todo), command.TodoId));
        }
        // Rule: Complete todos cannot be updated, except to update to not complete
        else if (!todo.CanBeUpdated)
        {
            return Result<UpdateTodoCategoryResponse>.DomainRuleFailure(DomainErrors.CannotUpdateCompletedTodosErrorMessage());
        }
        // Rule: Todo must have a category
        else if (todo.CategoryId is null)
        {
            return Result<UpdateTodoCategoryResponse>.DomainRuleFailure(DomainErrors.TodoCategoryIsNullErrorMessage());
        }
        else
        {
            await _repository.UpdateTodoCategoryAsync(todo, Identifier<Category>.Create(command.CategoryId));
            return Result<UpdateTodoCategoryResponse>.Success(new UpdateTodoCategoryResponse(true));
        }
    }
}
