
namespace TodoHelper.Domain.ValueObjects;

public sealed class DueDate
{
    public DateOnly? Value { get; }

    private DueDate(DateOnly? value) => Value = value;

    internal static DueDate CreateNew() => new(null);
    public static DueDate Create(DateOnly? value) => new(value);
    
}
