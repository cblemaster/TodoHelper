
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.Common;

internal class Predicates
{
    internal static Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => d => d.DueDate;
    internal static Func<TodoDTO, string> SortByDescriptionPredicate() => d => d.Description;
}
