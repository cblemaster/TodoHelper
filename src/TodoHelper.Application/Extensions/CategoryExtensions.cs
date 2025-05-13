
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Extensions;

internal static class CategoryExtensions
{
    internal static CategoryDTO MapToDTO(this Domain.Entities.Category category)
    {
        int count = category.Todos is null ? 0 : category.Todos.Count(t => !t.CompleteDate.HasValue);
        return new(category.Id.Value, category.Name.Value, count);
    }
}
