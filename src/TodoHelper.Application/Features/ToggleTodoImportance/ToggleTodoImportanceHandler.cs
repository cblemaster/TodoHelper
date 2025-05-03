
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.ToggleTodoImportance;

internal sealed class ToggleTodoImportanceHandler(ITodosRepository repository) : ICommandHandler<ToggleTodoImportanceCommand, ToggleTodoImportanceResponse>
{
    private readonly ITodosRepository _repository = repository;
    public Task<Result<ToggleTodoImportanceResponse>> HandleAsync(ToggleTodoImportanceCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodos().Single(t => t.Id.Value == command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<ToggleTodoImportanceResponse>.Failure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            _ = _repository.UpdateTodoImportanceAsync(todo);
            return Task.FromResult(Result<ToggleTodoImportanceResponse>.Success(new ToggleTodoImportanceResponse(true)));
        }
    }
}
