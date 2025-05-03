
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetCategories;

public class GetCategoriesCommand : ICommand<GetCategoriesResponse>
{
    public Expression<Func<Category, string>> OrderByPredicate { get; } = c => c.Name.Value;
}
