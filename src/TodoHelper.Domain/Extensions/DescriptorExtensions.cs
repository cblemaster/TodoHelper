
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
            return Result<Descriptor>.Failure(Error.StringValueNotValid(attribute));
        }
        if (maxLength < 1)
        {
            return Result<Descriptor>.Failure(Error.NotValid("Max length must be one (1) or more."));
        }

        string validationError = string.Empty;
        
        return string.IsNullOrWhiteSpace(descriptor.Value)
            ? Result<Descriptor>.Failure(Error.StringValueNotValid(attribute))
            : descriptorValue.Length > maxLength
                
                ? Result<Descriptor>.Failure(Error.StringLengthNotValid(attribute, maxLength))
                : validationError != string.Empty
                    
                    ? Result<Descriptor>.Failure(Error.NotValid(validationError))
                    : Result<Descriptor>.Success(descriptor);
    }
}
