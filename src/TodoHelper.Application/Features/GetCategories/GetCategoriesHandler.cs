
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
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
        categories.ForEach(c => dtos.Add(c.MapToDTO()));

        // Specification: Sorted by name
        _ = dtos.OrderBy(command.SortByNamePredicate());
        GetCategoriesResponse response = new(dtos);
        return Task.FromResult(Result<GetCategoriesResponse>.Success(response));
    }
}
