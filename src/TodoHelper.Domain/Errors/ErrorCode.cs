﻿
namespace TodoHelper.Domain.Errors;

public enum ErrorCode
{
    None = 0,
    NotFound = 1,
    NotValid = 2,
    AlreadyExists = 3,
    DomainRuleViolation = 4,
    Unknown = 99,
}
