
namespace TodoHelper.Domain.ValueObjects;

internal sealed class Importance
{
    internal bool IsImportant { get; }

    private Importance(bool isImportant) => IsImportant = isImportant;

    internal static Importance CreateNew(bool isImportant) => new(isImportant);
}
