
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateTodo;

internal sealed class CreateTodoHandler(ITodosRepository repository) : HandlerBase<CreateTodoCommand, CreateTodoResponse>(repository)
{
    public override async Task<Result<CreateTodoResponse>> HandleAsync(CreateTodoCommand command, CancellationToken cancellationToken = default)
    {
        // Rule: Todo must have a category (enforced by type system)

        Result<Todo> todoResult = Todo.CreateNew(command.CategoryId, command.Description, command.DueDate);

        // Rule: Todo description must not be null, an empty string, nor all-whitespace characters
        // Rule: Todo description must be 255 characters or fewer
        if (todoResult.IsFailure && todoResult.Error is string error)
        {
            return Result<CreateTodoResponse>.ValidationFailure(error);
        }
        else if (todoResult.IsSuccess && todoResult.Value is Todo todo)
        {
            await _repository.CreateTodoAsync(todo);
            return Result<CreateTodoResponse>.Success(new CreateTodoResponse(todo.MapToDTO()));
        }
        else
        {
            return Result<CreateTodoResponse>.UnknownFailure(DomainErrors.UnknownErrorMessage("creating todo"));
        }
    }
}
