
namespace TodoHelper.Domain.ValueObjects;

internal sealed class CloseDate
{
    internal DateTimeOffset? Value { get; }

    private CloseDate(DateTimeOffset? value) => Value = value;

    internal static CloseDate CreateNew(DateTimeOffset? value) => new(value);
}
