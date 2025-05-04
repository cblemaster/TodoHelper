
namespace TodoHelper.Domain.ValueObjects;

public sealed class Importance
{
    public bool IsImportant { get; }

    private Importance(bool isImportant) => IsImportant = isImportant;

    //internal static Importance CreateNew() => new(false);
    public static Importance Create(bool isImportant) => new(isImportant);
}
