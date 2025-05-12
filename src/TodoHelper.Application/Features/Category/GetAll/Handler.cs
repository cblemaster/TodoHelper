
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.GetAll;

internal class Handler(ITodosRepository<_Category> repository) : HandlerBase<Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        IEnumerable<CategoryDTO> dtos =
            (await _repository.GetAllAsync())
            .Select(c => c.MapToDTO())
            .OrderBy(c => c.Name);
        Response response = new(dtos);
        return Result<Response>.Success(response);
    }
}
