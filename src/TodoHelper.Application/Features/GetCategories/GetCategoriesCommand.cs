
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.GetCategories;

internal sealed class GetCategoriesCommand : ICommand<GetCategoriesResponse>
{
    internal Func<CategoryDTO, string> SortByNamePredicate() => d => d.Name;
}
