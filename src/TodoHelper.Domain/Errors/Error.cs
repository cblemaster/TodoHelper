
namespace TodoHelper.Domain.Errors;

public sealed record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
}
