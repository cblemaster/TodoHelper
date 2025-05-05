
using System.Linq.Expressions;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.GetCategories;

internal sealed class GetCategoriesCommand : ICommand<GetCategoriesResponse>
{
    internal Expression<Func<CategoryDTO, string>> OrderByPredicate { get; } = c => c.Name;
}
