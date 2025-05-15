
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.Delete;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Todo? entity = await _repository.GetByIdAsync(command.Id);
        if (entity is null)
        {
            return Result<Response>.Failure(Error.NotFound(nameof(_Todo)));
        }
        else
        {
            await _repository.DeleteAsync(entity);
            Response response = new();
            return Result<Response>.Success(response);
        }
    }
}
