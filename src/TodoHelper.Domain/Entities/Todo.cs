using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Todo : Entity<Todo>
{
    internal override Identifier<Todo> Id { get; }
    internal Category Category { get; } = default!;
    internal Identifier<Category> CategoryId { get; }
    internal Description Description { get; }
    internal DueDate DueDate { get; }
    internal CloseDate CloseDate { get; }
    internal CreateDate CreateDate { get; }
    internal UpdateDate UpdateDate { get; }
    internal Importance Importance { get; }
    internal bool IsComplete => CloseDate.Value is not null;
    internal bool CanBeUpdated => !IsComplete;
    internal bool CanBeDeleted => !Importance.IsImportant;

    private Todo
    (
        Identifier<Category> categoryId,
        Description description,
        DueDate dueDate,
        CloseDate closeDate,
        CreateDate createDate,
        UpdateDate updateDate,
        Importance importance
    )
    {
        Id = Identifier<Todo>.CreateNew();
        CategoryId = categoryId;
        Description = description;
        DueDate = dueDate;
        CloseDate = closeDate;
        CreateDate = createDate;
        UpdateDate = updateDate;
        Importance = importance;
    }

    internal static Todo CreateNew(Guid categoryId, string description, DateOnly? dueDate, bool isImportant)
    {
        Result<Description> descriptionResult = Description.CreateNew(description);

        return descriptionResult.IsSuccess && descriptionResult.Value is not null
            ? new
            (
                Identifier<Category>.Create(categoryId),
                descriptionResult.Value,
                DueDate.CreateNew(dueDate),
                CloseDate.CreateNew(null),
                CreateDate.CreateNew(),
                UpdateDate.CreateNew(),
                Importance.CreateNew(isImportant)
            )
            : descriptionResult.IsFailure && descriptionResult.Error is not null
                ? InvalidTodoDescription(descriptionResult.Error)
                : InvalidTodoDescription("An unknown error occurred while creating a new todo.");
    }

    internal static Todo InvalidTodoDescription(string error) => CreateNew(Guid.Empty, error, null, false);
}
