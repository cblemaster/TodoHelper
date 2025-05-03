
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetCategories;

internal sealed class GetCategoriesCommand : ICommand<GetCategoriesResponse>
{
    internal Expression<Func<Category, string>> OrderByPredicate { get; } = c => c.Name.Value;
}
