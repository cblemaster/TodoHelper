
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Extensions;

internal static class CategoryExtensions
{
    internal static CategoryDTO MapToDTO(this Category entity) =>
        new(entity.Id.Value, entity.Name.Value, entity.Todos.Count(t => !t.IsComplete),
            entity.CreateDate.Value, entity.UpdateDate.Value);
}
