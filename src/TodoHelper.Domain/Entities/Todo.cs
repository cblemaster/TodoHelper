
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

    public bool IsComplete() => CompleteDate.MapToNullableDateTimeOffset().HasValue;
    public bool IsImportant() => Importance.IsImportant;
    public bool DueDateHasValue() => DueDate.MapToNullableDateOnly().HasValue;
    public bool HasGivenDueDate(DueDate given) => DueDateHasValue() && DueDate.MapToNullableDateOnly() == given.Value;
    public bool HasDueDateBeforeGiven(DueDate given) => DueDateHasValue() && DueDate.MapToNullableDateOnly() < given.Value;
    public bool HasGivenCategory(Category given) => Category == given;
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

    #region Factory methods

    private static Result<Todo> Create(Identifier<Todo> id, Category category,
        Identifier<Category> categoryId, string description, DueDate? dueDate,
        CompleteDate? completeDate, Importance importance)
    {
        Descriptor descriptionDescriptor =
            new
            (
                Value: description,
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
        };
    }

    public static Result<Todo> CreateNew(Category category, Identifier<Category> categoryId,
        string description, DateOnly? dueDate) =>
            Create(Identifier<Todo>.CreateNew(), category, categoryId, description,
                new DueDate(dueDate), completeDate: null, importance: new(false));

    public static Result<Todo> CreateWithNew(Identifier<Todo> id, Category category,
        Identifier<Category> categoryId, string description, DateOnly? dueDate,
        DateTimeOffset? completeDate, bool isImportant) =>
            Create(id, category, categoryId, description, new DueDate(dueDate),
                new CompleteDate(completeDate), new Importance(isImportant));

    #endregion Factory methods

    #region Predicates
    #region Command predicates
    public static Func<Todo, bool> CanDelete() =>
        todo => !todo.Importance.IsImportant;
    public static Func<Todo, bool> CanUpdate() =>
        todo => !todo.CompleteDate.HasValue;
    #endregion Command predicates

    #region Key predicates
    public static Func<Todo, DateOnly?> NullableDueDateKey() =>
        (todo) => todo.DueDate.MapToNullableDateOnly();
    public static Func<Todo, string> DescriptionKey() =>
        (todo) => todo.Description.Value;
    public static Func<Todo, bool> IsCompleteKey() =>
        (todo) => todo.CompleteDate.MapToNullableDateTimeOffset().HasValue;
    #endregion Key predicates

    #region Filter predicates
    public static Func<Todo, bool> GetTodosImportant() =>
        todo => GetTodosByImportance(new Importance(true)).Invoke(todo);
    public static Func<Todo, bool> GetTodosDueToday(DueDate today) =>
        todo => GetTodosByDueDate(today).Invoke(todo);
    public static Func<Todo, bool> GetTodosOverdue(DueDate today) =>
        todo => todo.DueDate.HasValue &&
                todo.DueDate.Value.Value.HasValue &&
                    today.Value.HasValue &&
                    todo.DueDate.Value.Value < today.Value.Value;
    public static Func<Todo, bool> GetTodosCompleted() =>
        todo => IsCompleteKey().Invoke(todo);
    public static Func<Todo, bool> GetTodosNotCompleted() =>
        todo => !GetTodosCompleted().Invoke(todo);
    private static Func<Todo, bool> GetTodosByImportance(Importance importance) =>
        todo => todo.Importance == importance;
    private static Func<Todo, bool> GetTodosByDueDate(DueDate dueDate) =>
        todo => todo.DueDate.HasValue && todo.DueDate.Value == dueDate;
    #endregion Filter predicates
    #endregion Predicates
}
