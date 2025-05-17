
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
namespace TodoHelper.Application.Features.Todo.GetAll;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        IEnumerable<TodoDTO> dtos =
            (await _repository.GetAllAsync())
            .Select(c => c.MapToDTO())
            .OrderBy(c => c.Description);
        Response response = new(dtos);
        return Result<Response>.Success(response);
    }
}
