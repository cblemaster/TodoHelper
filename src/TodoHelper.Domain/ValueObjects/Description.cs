
using TodoHelper.Domain.Rules;

namespace TodoHelper.Domain.ValueObjects;

internal sealed class Description
{
    internal string Value { get; }

    private Description(string value) => Value = value;

    internal static Description CreateNew(string value)
    {
        value = value.ReturnWithValidDescriptionOrThrow();
        return new(value);
    }
}
