
namespace TodoHelper.Domain.ValueObjects;

internal sealed class DueDate
{
    internal DateOnly? Value { get; }

    private DueDate(DateOnly? value) => Value = value;

    internal static DueDate CreateNew(DateOnly? value) => new(value);
}
