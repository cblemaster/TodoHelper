
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects.Extensions;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.GetAllByCategory;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command)
    {
        Func<_Todo, bool> completeFilter = t => command.IncludeComplete || !t.IsComplete();

        IEnumerable<TodoDTO> dtos =
            (await _repository
                .GetAllAsync2()
                .Where(t => t.HasGivenCategory(command.CategoryId))
                .Where(completeFilter)
                .OrderByDescending(t => t.DueDate.MapToNullableDateOnly())
                .ThenBy(t => t.Description.Value)
                .AsQueryable()
                .AsNoTracking()
                .ToListAsync()
            )
            .Select(t => t.MapToDTO());

        return new Response(Result<IEnumerable<TodoDTO>>.Success(dtos));
    }
}
