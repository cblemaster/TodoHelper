
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateTodo;

internal sealed class CreateTodoHandler(ITodosRepository repository) : HandlerBase<CreateTodoCommand, CreateTodoResponse>(repository)
{
    public override async Task<Result<CreateTodoResponse>> HandleAsync(CreateTodoCommand command, CancellationToken cancellationToken = default)
    {
        Result<Todo> todoResult = Todo.CreateNew(command.CategoryId, command.Description, command.DueDate);

        if (todoResult.IsFailure && todoResult.Error is not null)
        {
            return Result<CreateTodoResponse>.Failure(todoResult.Error);
        }
        else if (todoResult.IsSuccess && todoResult.Value is Todo todo)
        {
            await _repository.CreateTodoAsync(todo);
            return Result<CreateTodoResponse>.Success(new CreateTodoResponse(new TodoDTO(todo.Id.Value, todo.Category.Name.Value, todo.CategoryId.Value, todo.Description.Value, todo.DueDate.Value, todo.CompleteDate.Value, todo.CreateDate.Value, todo.UpdateDate.Value, todo.Importance.IsImportant)));
        }
        else
        {
            return Result<CreateTodoResponse>.Failure("An unknown error occurred when creating the todo.");
        }
    }
}
