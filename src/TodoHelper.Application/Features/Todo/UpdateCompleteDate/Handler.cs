
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.UpdateCompleteDate;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public async override Task<Response> HandleAsync(Command command)
    {
        _Todo? entity = await _repository.GetByIdAsync(Identifier<_Todo>.Create(command.Id));
        if (entity is null)
        {
            return new Response(Result<bool>.Failure(Error.NotFound(nameof(_Todo))));
        }
        else
        {
            Result<_Todo> result = _Todo.CreateWithNewCompleteDate
            (
                    entity.Id,
                    entity.Category,
                    entity.CategoryId,
                    entity.Description,
                    entity.DueDate,
                    new CompleteDate(command.CompleteDate),
                    entity.Importance
                );

            if (result.IsFailure && result.Error is Error error)
            {
                return new Response(Result<bool>.Failure(Error.NotValid(error.Description)));
            }
            else if (result.IsSuccess && result.Payload is _Todo todo)
            {
                await _repository.UpdateAsync(todo);
                return new Response(Result<bool>.Success(true));
            }
            else
            {
                return new Response(Result<bool>.Failure(Error.Unknown));
            }
        }
    }
}
