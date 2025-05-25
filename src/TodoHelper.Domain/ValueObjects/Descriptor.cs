
namespace TodoHelper.Domain.ValueObjects;

public record struct Descriptor(string StringValue, uint MaxLength, string AttributeName,
    bool IsUnique);
