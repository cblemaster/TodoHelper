
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Application.Features.UpdateTodoCategory;

internal sealed class UpdateTodoCategoryHandler(ITodosRepository repository) : HandlerBase<UpdateTodoCategoryCommand, UpdateTodoCategoryResponse>(repository)
{
    public override Task<Result<UpdateTodoCategoryResponse>> HandleAsync(UpdateTodoCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodos().Single(t => t.Id.Value == command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoCategoryResponse>.Failure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            _ = _repository.UpdateTodoCategoryAsync(todo, Identifier<Category>.Create(command.CategoryId));
            return Task.FromResult(Result<UpdateTodoCategoryResponse>.Success(new UpdateTodoCategoryResponse(true)));
        }
    }
}
