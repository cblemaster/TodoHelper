
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Application.Features.UpdateTodoCategory;

internal sealed class UpdateTodoCategoryHandler(ITodosRepository repository) : ICommandHandler<UpdateTodoCategoryCommand, UpdateTodoCategoryResponse>
{
    private readonly ITodosRepository _repository = repository;
    public Task<Result<UpdateTodoCategoryResponse>> HandleAsync(UpdateTodoCategoryCommand command, CancellationToken cancellationToken = default)
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
