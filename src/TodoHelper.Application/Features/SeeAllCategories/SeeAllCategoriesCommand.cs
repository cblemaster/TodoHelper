
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.SeeAllCategories;

public class SeeAllCategoriesCommand : ICommand<SeeAllCategoriesResponse>
{
    public Expression<Func<Category, string>> SortPredicate { get; } = c => c.Name.Value;
}
