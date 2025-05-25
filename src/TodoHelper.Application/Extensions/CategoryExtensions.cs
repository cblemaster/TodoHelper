
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Extensions;

internal static class CategoryExtensions
{
    internal static CategoryDTO MapToDTO(this Category category)
    {
        uint count = Convert.ToUInt32(category.Todos is null ? 0 : category.Todos.Count(t => !t.CompleteDate.HasValue));
        return new(category.Id.GuidValue, category.Name.StringValue, count);
    }
}
