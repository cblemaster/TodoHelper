
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.GetAll;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command)
    {
        IEnumerable<CategoryDTO> dtos =
            (await _repository
                .GetAllAsyncQueryable()
                .Include(c => c.Todos.Where(t => !t.CompleteDateHasValue()))
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .ToListAsync()
            )
            .Select(c => c.MapToDTO());

        return new Response(Result<IEnumerable<CategoryDTO>>.Success(dtos));
    }
}
