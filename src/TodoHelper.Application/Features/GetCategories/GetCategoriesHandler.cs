
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetCategories;

internal sealed class GetCategoriesHandler(ITodosRepository repository) : HandlerBase<GetCategoriesCommand, GetCategoriesResponse>(repository)
{
    public override Task<Result<GetCategoriesResponse>> HandleAsync(GetCategoriesCommand command, CancellationToken cancellationToken = default)
    {
        List<CategoryDTO> dtos = [];
        List<Category> categories = [.. _repository.GetCategories()];
        categories.ForEach(c => dtos.Add(new CategoryDTO(c.Id.Value, c.Name.Value, c.Todos.Count(t => !t.IsComplete), c.CreateDate.Value, c.UpdateDate.Value)));
        _ = dtos.OrderBy(command.OrderByPredicate.Compile());
        GetCategoriesResponse response = new(dtos);
        return Task.FromResult(Result<GetCategoriesResponse>.Success(response));
    }
}
