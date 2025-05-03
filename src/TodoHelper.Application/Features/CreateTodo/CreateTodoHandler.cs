
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateTodo;

internal sealed class CreateTodoHandler(ITodosRepository repository) : ICommandHandler<CreateTodoCommand, CreateTodoResponse>
{
    private readonly ITodosRepository _repository = repository;

    public async Task<Result<CreateTodoResponse>> HandleAsync(CreateTodoCommand command, CancellationToken cancellationToken = default)
    {
        Result<Todo> todoResult = Todo.CreateNew(command.CategoryId, command.Description, command.DueDate, command.IsImportant);
        
        if (todoResult.IsFailure && todoResult.Error is not null)
        {
            return Result<CreateTodoResponse>.Failure(todoResult.Error);
        }
        else if (todoResult.IsSuccess && todoResult.Value is not null)
        {
            await _repository.CreateTodoAsync(todoResult.Value);
            return Result<CreateTodoResponse>.Success(new CreateTodoResponse(todoResult.Value));
        }
        else
        {
            return Result<CreateTodoResponse>.Failure("An unknown error occurred when creating the todo.");
        }
    }
}
