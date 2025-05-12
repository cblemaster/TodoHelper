
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Extensions;

internal static class DescriptorExtensions
{
    internal static Result<Descriptor> Validate(this Descriptor descriptor)
    {
        string descriptorValue = descriptor.Value;
        string attribute = descriptor.AttributeName;
        int maxLength = descriptor.MaxLength;

        if (string.IsNullOrEmpty(attribute))
        {
            attribute = "<field name>"; // TODO: return failure result
        }
        if (maxLength < 1)
        {
            maxLength = 1; // TODO: return failure result
        }

        string validationError = string.Empty;
        if (string.IsNullOrWhiteSpace(descriptor.Value))
        {
            validationError += $"\n{attribute} is required and cannot consist exclusively of whitespace characters.";
        }
        if (descriptorValue.Length > maxLength)
        {
            validationError += $"\n{attribute} must be {maxLength} characters or fewer.";
        }

        return validationError != string.Empty
            ? Result<Descriptor>.Failure(DescriptorErrors.NotValid(validationError))
            : Result<Descriptor>.Success(descriptor);
    }
}
