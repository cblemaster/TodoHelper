
namespace TodoHelper.Domain.ValueObjects;

public record struct Descriptor(string Value, uint MaxLength, string AttributeName, bool IsUnique);
