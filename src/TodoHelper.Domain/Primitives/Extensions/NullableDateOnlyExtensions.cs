
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Primitives.Extensions;

public static class NullableDateOnlyExtensions
{
    public static DueDate? ToNullableDueDate(this DateOnly? dateOnly) =>
        dateOnly is not null
            ? new(dateOnly.Value)
            : null;
    // TODO: does this have any callers?
}
