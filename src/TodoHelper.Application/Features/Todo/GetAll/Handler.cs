
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects.Extensions;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.GetAll;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command)
    {
        IEnumerable<TodoDTO> dtos =
            (await _repository
                .GetAllAsync2()
                .OrderByDescending(t => t.DueDate.ToNullableDateOnly())
                .ThenBy(t => t.Description.StringValue)
                .AsNoTracking()
                .ToListAsync()
            )
            .Select(t => t.MapToDTO());

        return new Response(Result<IEnumerable<TodoDTO>>.Success(dtos));
    }
}
