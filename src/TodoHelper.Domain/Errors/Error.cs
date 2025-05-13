
namespace TodoHelper.Domain.Errors;

public sealed record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error Unknown = new("An unknown error occurred.");
    public static readonly Error NotFound = new("The requested resource cannot be found.");
}
