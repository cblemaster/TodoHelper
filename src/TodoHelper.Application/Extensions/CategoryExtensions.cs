
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Extensions;

internal static class CategoryExtensions
{
    internal static CategoryDTO MapToDTO(this Category category)
    {
        int count = category.Todos is null ? 0 : category.Todos.Count(t => !t.CompleteDate.HasValue);
        return new(category.Id.Value, category.Name.Value, count);
    }
}
