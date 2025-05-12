
namespace TodoHelper.Domain.Errors;

public static class DescriptorErrors
{
    public static Error NotValid(string error) => new(error);
    public static Error AttributeNameNotValid() => new("Attribute name is required.");
    public static Error MaxLengthNotValid() => new("Max length must be at least 1.");
}
