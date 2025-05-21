
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Definitions;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;
using TodoHelper.Domain.ValueObjects.Extensions;

namespace TodoHelper.Domain.Entities;

public sealed class Todo : Entity<Todo>
{
    #region Properties
    public override Identifier<Todo> Id { get; }
    public Category Category { get; } = default!;
    public Identifier<Category> CategoryId { get; }
    public Descriptor Description { get; }
    public DueDate? DueDate { get; }
    public CompleteDate? CompleteDate { get; }
    public Importance Importance { get; }
    #endregion Properties

    #region Constructors
#pragma warning disable CS8618
    private Todo() { }
#pragma warning restore CS8618

    private Todo(Identifier<Todo> id, Identifier<Category> categoryId, Descriptor description,
        DueDate? dueDate, CompleteDate? completeDate, Importance importance)
    {
        Id = id;
        CategoryId = categoryId;
        Description = description;
        DueDate = dueDate;
        CompleteDate = completeDate;
        Importance = importance;
    }

    private Todo(Identifier<Todo> id, Category category, Identifier<Category> categoryId,
        Descriptor description, DueDate? dueDate, CompleteDate? completeDate,
        Importance importance)
        : this(id, categoryId, description, dueDate, completeDate, importance) => Category = category;

    #endregion Constructors

    #region Factory

    private static Result<Todo> Create(Identifier<Todo> id, Category category,
        Identifier<Category> categoryId, string description, DueDate? dueDate,
        CompleteDate? completeDate, Importance importance)
    {
        Descriptor descriptionDescriptor = new(Value: description,
            DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH,
            DataDefinitions.TODO_DESCRIPTION_ATTRIBUTE,
            DataDefinitions.IS_TODO_DESCRIPTION_UNIQUE);
        Result<Descriptor> result = descriptionDescriptor.GetValidDescriptorOrValidationError();

        if (result.IsFailure && result.Error is Error error)
        {
            return Result<Todo>.Failure(Error.NotValid(error.Description));
        }
        else if (result.IsSuccess && result.Payload is Descriptor descriptor)
        {
            Todo todo = new(id, category, categoryId, descriptor, dueDate, completeDate, importance);
            return Result<Todo>.Success(todo);
        }
        else
        {
            return Result<Todo>.Failure(Error.Unknown);
        }
    }

    private static Result<Todo> CreateNew(Category category, Identifier<Category> categoryId,
        string description, DateOnly? dueDate) =>
            Create(Identifier<Todo>.CreateNew(), category, categoryId, description,
                new DueDate(dueDate), completeDate: null, importance: new(false));

    private static Result<Todo> CreateWithNew(Identifier<Todo> id, Category category,
        Identifier<Category> categoryId, string description, DateOnly? dueDate,
        DateTimeOffset? completeDate, bool isImportant) =>
            Create(id, category, categoryId, description, new DueDate(dueDate),
                new CompleteDate(completeDate), new Importance(isImportant));

    #endregion Factory
}
