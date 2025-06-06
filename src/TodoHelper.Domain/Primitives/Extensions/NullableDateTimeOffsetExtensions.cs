﻿
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Primitives.Extensions;

public static class NullableDateTimeOffsetExtensions
{
    public static CompleteDate? ToNullableCompleteDate(this DateTimeOffset? dateTimeOffset) =>
        dateTimeOffset is not null
            ? new(dateTimeOffset.Value)
            : null;
}
