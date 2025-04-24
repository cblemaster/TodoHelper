
namespace TodoHelper.Domain.ValueObjects;

internal sealed class Name
{
    private const int MAX_LENGTH = 40;

    internal string Value { get; }

    private Name(string value) => Value = value;

    // TODO: Validation
    internal static Name CreateNew(string value) => new(value);
}
