
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Extensions;

public static class CompleteDateExtensions
{
    public static DateTimeOffset? ToNullableDateTimeOffset(this CompleteDate? completeDate) =>
        completeDate is not null && completeDate.Value.Value is not null
            ? completeDate.Value.Value
            : null;
}
