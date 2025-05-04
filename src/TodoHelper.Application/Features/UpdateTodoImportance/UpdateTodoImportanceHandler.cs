
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.ToggleTodoImportance;

internal sealed class UpdateTodoImportanceHandler(ITodosRepository repository) : ICommandHandler<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse>
{
    private readonly ITodosRepository _repository = repository;
    public Task<Result<UpdateTodoImportanceResponse>> HandleAsync(UpdateTodoImportanceCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodos().Single(t => t.Id.Value == command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoImportanceResponse>.Failure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            _ = _repository.UpdateTodoImportanceAsync(todo);
            return Task.FromResult(Result<UpdateTodoImportanceResponse>.Success(new UpdateTodoImportanceResponse(true)));
        }
    }
}
