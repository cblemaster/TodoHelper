
namespace TodoHelper.Domain.Errors;

public sealed record Error(ErrorCode ErrorCode, string Description)
{
    public static readonly Error None = new(ErrorCode.None, string.Empty);
    
    public static readonly Error Unknown = new(ErrorCode.Unknown, "An unknown error occurred.");
    
    public static Error NotFound(string resource) =>
        new(ErrorCode.NotFound, $"The requested {resource} cannot be found.");
    
    public static Error NotValid(string description) =>
        new(ErrorCode.NotValid, description);
    
    public static Error AlreadyExists(string attribute, string value) =>
        new(ErrorCode.AlreadyExists, $"{attribute} \"{value}\" already exists.");
    
    internal static Error StringValueNotValid(string attribute) =>
        new(ErrorCode.NotValid, $"{attribute} is required and cannot consist of exclusively" +
            $" whitespace characters.");
    
    internal static Error StringLengthNotValid(string attribute, uint maxLength) =>
        new(ErrorCode.NotValid, $"{attribute} must be {maxLength} characters or fewer.");
}
