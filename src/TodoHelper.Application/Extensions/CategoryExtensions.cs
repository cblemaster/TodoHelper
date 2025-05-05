
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Extensions;

internal static class CategoryExtensions
{
    internal static CategoryDTO MapToDTO(this Category category) =>
        new(category.Id.Value, category.Name.Value, category.Todos.Count(t => !t.IsComplete),
            category.CreateDate.Value, category.UpdateDate.Value);
}
