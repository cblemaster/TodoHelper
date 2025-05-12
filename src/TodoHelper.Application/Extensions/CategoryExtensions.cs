
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Extensions;

internal static class CategoryExtensions
{
    internal static CategoryDTO MapToDTO(this Domain.Entities.Category category) =>
        new(category.Id.Value, category.Name.Value, category.Todos.Count(t => !t.CompleteDate.HasValue));
}
