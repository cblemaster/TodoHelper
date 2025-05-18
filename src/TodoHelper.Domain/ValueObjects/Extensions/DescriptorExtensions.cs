
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Primitives.Extensions;
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects.Extensions;

public static class DescriptorExtensions
{
    public static Result<Descriptor> ValidateDescriptor(this Descriptor descriptor)
    {
        string descriptorValue = descriptor.Value;

        // TODO: inform user that defaults are applied...
        string attribute =
            descriptor
            .AttributeName
            .MapToAttributeNameValueOrDefault(descriptor.IsUnique);
        
        uint maxLength = descriptor.MaxLength == 0 ? 1 : descriptor.MaxLength;

        return !descriptorValue.IsValueValid()
            ? Result<Descriptor>.Failure(Error.StringValueNotValid(attribute))
            
            : descriptorValue.IsLengthValid(maxLength)
                ? Result<Descriptor>.Failure(Error.StringLengthNotValid(attribute, maxLength))
                : Result<Descriptor>.Success(descriptor);
    }

    private static string MapToAttributeNameValueOrDefault(this string s, bool isUnique)
    {
        if (!s.IsValueValid())
        {
            s = isUnique
                ? $"<Unknown attribute {Guid.NewGuid()}>"
                : "<Unknown attribute>";
        }
        return s;
    }
}
