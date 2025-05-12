
namespace TodoHelper.Domain.Errors;

internal static class DescriptorErrors
{
    internal static Error NotValid(string error) => new(error);
}
