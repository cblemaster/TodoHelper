
namespace TodoHelper.Domain.ValueObjects;

public sealed class DueDate
{
    public DateOnly? Value { get; }

    private DueDate(DateOnly? value) => Value = value;

    public static DueDate Create(DateOnly? value) => new(value);
}
