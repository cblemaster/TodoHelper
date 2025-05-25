
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

    public bool CompleteDateHasValue() => CompleteDate.ToNullableDateTimeOffset().HasValue;
    private bool DueDateHasValue() => DueDate.ToNullableDateOnly().HasValue;
    public bool HasGivenCategoryId(Identifier<Category> given) => CategoryId == given;
    public bool HasGivenDueDate(DueDate given) =>
        DueDateHasValue() && DueDate.ToNullableDateOnly() == given.DateValue;
    public bool HasDueDateBeforeGiven(DueDate given) =>
        DueDateHasValue() && DueDate.ToNullableDateOnly() < given.DateValue;
    public bool IsImportant() => Importance.BoolValue;
    


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
        : this(id, categoryId, description, dueDate, completeDate, importance) =>
            Category = category;

    #endregion Constructors

    #region Factory methods
    private static Result<Todo> Create(Identifier<Todo> id, Category category,
        Identifier<Category> categoryId, string description, DueDate? dueDate,
        CompleteDate? completeDate, Importance importance)
    {
        // TODO: replace validation with pattern matching
        Descriptor descriptionDescriptor =
            new
            (
                description,
                DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH,
                DataDefinitions.TODO_DESCRIPTION_ATTRIBUTE,
                DataDefinitions.IS_TODO_DESCRIPTION_UNIQUE
            );
        Result<Descriptor> result = descriptionDescriptor.GetValidDescriptorOrValidationError();

        switch (result)
        {
            case Result<Descriptor> failure
                when failure.IsFailure &&
                    failure.Error is Error error:
                return Result<Todo>.Failure(Error.NotValid(error.Description));
            case Result<Descriptor> success
                when success.IsSuccess &&
                    success.Payload is Descriptor descriptor:
                    {
                        Todo todo = new(id, category, categoryId, descriptor,
                            dueDate, completeDate, importance);
                        return Result<Todo>.Success(todo);
                    }
            default:
                return Result<Todo>.Failure(Error.Unknown);
        }
    }

    public static Result<Todo> CreateNew(Category category, Identifier<Category> categoryId,
        string description, DateOnly? dueDate) =>
            Create(Identifier<Todo>.CreateNew(), category, categoryId, description,
                new DueDate(dueDate), completeDate: null, importance: new(false));

    public static Result<Todo> CreateWithNew(Identifier<Todo> id, Category category,
        Identifier<Category> categoryId, string description, DateOnly? dueDate,
        CompleteDate? completeDate, bool isImportant) =>
            Create(id, category, categoryId, description, new DueDate(dueDate),
                completeDate, new Importance(isImportant));

    public static Result<Todo> CreateWithNewCompleteDate(Identifier<Todo> id, Category category,
        Identifier<Category> categoryId, Descriptor description, DueDate? dueDate,
        CompleteDate completeDate, Importance importance) =>
            Create(id, category, categoryId, description.StringValue, dueDate, completeDate, importance);
    #endregion Factory methods
}
