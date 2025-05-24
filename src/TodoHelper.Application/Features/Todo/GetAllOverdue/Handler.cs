
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;
using TodoHelper.Domain.ValueObjects.Extensions;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.GetAllOverdue;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        Func<_Todo, bool> completeFilter = t => command.IncludeComplete || !t.IsComplete();

        DateOnly? today = DateOnly.FromDateTime(DateTime.Today);

        IEnumerable<TodoDTO> dtos =
            (await _repository
                .GetAllAsync2()
                .Where(t => t.HasDueDateBeforeGiven(new DueDate(today)))
                .Where(completeFilter)
                .OrderByDescending(t => t.DueDate.MapToNullableDateOnly())
                .ThenBy(t => t.Description.Value)
                .AsQueryable()
                .AsNoTracking()
                .ToListAsync(cancellationToken)
            )
            .Select(t => t.MapToDTO());

        return new Response(Result<IEnumerable<TodoDTO>>.Success(dtos));
    }
}
