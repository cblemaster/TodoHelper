
namespace TodoHelper.Domain.Errors;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new("none", string.Empty);
}
