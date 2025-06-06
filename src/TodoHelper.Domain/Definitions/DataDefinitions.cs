﻿
namespace TodoHelper.Domain.Definitions;

public static class DataDefinitions
{
    public const int CATEGORY_NAME_MAX_LENGTH = 40;
    public const int TODO_DESCRIPTION_MAX_LENGTH = 255;

    public const string CATEGORY_NAME_ATTRIBUTE = "Category name";
    public const string TODO_DESCRIPTION_ATTRIBUTE = "Todo description";

    public const bool IS_CATEGORY_NAME_UNIQUE = true;
    public const bool IS_TODO_DESCRIPTION_UNIQUE = false;

    public const bool IS_UNICODE_DEFAULT_VALUE = false;
}
