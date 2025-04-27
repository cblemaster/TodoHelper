
namespace TodoHelper.Domain.ValueObjects;

public sealed class Importance
{
    public bool IsImportant { get; }

    private Importance(bool isImportant) => IsImportant = isImportant;

    internal static Importance CreateNew(bool isImportant) => new(isImportant);
}
