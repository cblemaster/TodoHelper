
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Extensions;

public static class DescriptorExtensions
{
    public static Result<Descriptor> Validate(this Descriptor descriptor)
    {
        string descriptorValue = descriptor.Value;
        string attribute = descriptor.AttributeName;
        int maxLength = descriptor.MaxLength;

        if (string.IsNullOrEmpty(attribute))
        {
            return Result<Descriptor>.Failure(DescriptorErrors.AttributeNameNotValid());
        }
        if (maxLength < 1)
        {
            return Result<Descriptor>.Failure(DescriptorErrors.MaxLengthNotValid());
        }

        string validationError = string.Empty;
        if (string.IsNullOrWhiteSpace(descriptor.Value))
        {
            validationError = $"{attribute} is required and cannot consist exclusively of whitespace characters.";
        }
        else if (descriptorValue.Length > maxLength)
        {
            validationError = $"{attribute} must be {maxLength} characters or fewer.";
        }

        return validationError != string.Empty
            ? Result<Descriptor>.Failure(DescriptorErrors.NotValid(validationError))
            : Result<Descriptor>.Success(descriptor);
    }
}
