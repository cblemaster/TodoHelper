
namespace TodoHelper.Domain.ValueObjects;

internal sealed class Description
{
    private const int MAX_LENGTH = 255;

    internal string Value { get; }

    private Description(string value) => Value = value;

    // TODO: Validation
    internal static Description CreateNew(string value) => new(value);
}
