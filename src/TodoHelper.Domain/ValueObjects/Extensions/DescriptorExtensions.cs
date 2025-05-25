
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Primitives.Extensions;
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects.Extensions;

internal static class DescriptorExtensions
{
    internal static Result<Descriptor> GetValidDescriptorOrValidationError(this Descriptor descriptor)
    {
        string descriptorValue = descriptor.StringValue;

        // TODO: I'm not very fond of this implementation;
        //   at the very least, inform the user that defaults will be applied
        string attribute =
            descriptor
            .AttributeName
            .GetAttributeNameValueOrDefault(descriptor.IsUnique);

        uint maxLength = descriptor.MaxLength == 0 ? 1 : descriptor.MaxLength;

        // TODO: replace string validation with pattern matching
        return !descriptorValue.IsValueValid()
            ? Result<Descriptor>.Failure(Error.StringValueNotValid(attribute))

            : descriptorValue.IsLengthValid(maxLength)
                ? Result<Descriptor>.Failure(Error.StringLengthNotValid(attribute, maxLength))
                : Result<Descriptor>.Success(descriptor);
    }

    private static string GetAttributeNameValueOrDefault(this string s, bool isUnique)
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
