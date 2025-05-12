
namespace TodoHelper.Domain.Errors;

public static class DescriptorErrors
{
    public static Error NotValid(string error) => new(error);
}
