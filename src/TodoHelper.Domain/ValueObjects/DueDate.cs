
namespace TodoHelper.Domain.ValueObjects;

public sealed class DueDate
{
    public DateOnly? Value { get; }

    private DueDate(DateOnly? value) => Value = value;

    internal static DueDate CreateNew(DateOnly? value) => new(value);
}
