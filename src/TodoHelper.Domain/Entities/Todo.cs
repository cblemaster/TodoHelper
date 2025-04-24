
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Todo : Entity<Todo>
{
    internal override Identifier<Todo> Id { get; }
    internal Category Category { get; }
    internal Identifier<Category> CategoryId { get; }
    internal Description Description { get; }
    internal DueDate DueDate { get; }
    internal CloseDate CloseDate { get; }
    internal CreateDate CreateDate { get; }
    internal UpdateDate UpdateDate { get; }
    internal Importance Importance { get; }
    internal bool IsComplete => CloseDate.Value is not null;

    private Todo
    (
        Category category,
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
        Category = category;
        CategoryId = categoryId;
        Description = description;
        DueDate = dueDate;
        CloseDate = closeDate;
        CreateDate = createDate;
        UpdateDate = updateDate;
        Importance = importance;
    }

    internal static Todo CreateNew
    (
        Category category,
        Identifier<Category> categoryId,
        Description description,
        DueDate dueDate,
        CloseDate closeDate,
        CreateDate createDate,
        UpdateDate updateDate,
        Importance importance
    ) =>
        new
        (
            category,
            categoryId,
            description,
            dueDate,
            closeDate,
            createDate,
            updateDate,
            importance
        );
}
