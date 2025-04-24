using TodoHelper.Domain.ValueObjects.Rules;

namespace TodoHelper.Domain.ValueObjects;

internal sealed class Name
{
    private const int MAX_LENGTH = 40;

    internal string Value { get; }

    private Name(string value) => Value = value;

    internal static Name CreateNew(string value)
    {
        value = value.ReturnCategoryWithValidNameValueOrThrow();    // TODO: handle ex
        return new(value);
    }
}
