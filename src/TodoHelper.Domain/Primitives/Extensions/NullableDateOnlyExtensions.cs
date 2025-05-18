
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Primitives.Extensions;

public static class NullableDateOnlyExtensions
{
    public static DueDate? MapToNullableDueDate(this DateOnly? dateOnly) =>
        dateOnly is not null
            ? new(dateOnly.Value)
            : null;
}
