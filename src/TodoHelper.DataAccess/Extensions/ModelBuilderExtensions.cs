
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Definitions;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Primitives.Extensions;
using TodoHelper.Domain.ValueObjects;
using TodoHelper.Domain.ValueObjects.Extensions;

namespace TodoHelper.DataAccess.Extensions;

internal static class ModelBuilderExtensions
{
    internal static ModelBuilder ConfigureCategoryEntity(this ModelBuilder builder) =>
        builder.Entity<Category>(entity =>
        {
#pragma warning disable IDE0058
            entity.ToTable(nameof(Category));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasConversion(i => i.GuidValue, i => Identifier<Category>.Create(i));
            entity.Property(e => e.Name)
                .HasConversion(n => n.StringValue,
                    n => new Descriptor(n, DataDefinitions.CATEGORY_NAME_MAX_LENGTH,
                        DataDefinitions.CATEGORY_NAME_ATTRIBUTE,
                        DataDefinitions.IS_CATEGORY_NAME_UNIQUE));
            entity.Property(e => e.Name)
                .HasMaxLength(DataDefinitions.CATEGORY_NAME_MAX_LENGTH)
                .IsUnicode(DataDefinitions.IS_UNICODE_DEFAULT_VALUE);
            entity.HasIndex(e => e.Name)
                .IsUnique();
#pragma warning restore IDE0058
        });
    internal static ModelBuilder ConfigureTodoEntity(this ModelBuilder builder) =>
        builder.Entity<Todo>(entity =>
        {
#pragma warning disable IDE0058
            entity.ToTable(nameof(Todo));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasConversion(i => i.GuidValue, i => Identifier<Todo>.Create(i));
            entity.Property(e => e.CategoryId)
                .HasConversion(c => c.GuidValue, c => Identifier<Category>.Create(c));
            entity.Property(e => e.Description)
                .HasConversion(d => d.StringValue,
                    d => new Descriptor(d, DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH,
                        DataDefinitions.TODO_DESCRIPTION_ATTRIBUTE,
                        DataDefinitions.IS_TODO_DESCRIPTION_UNIQUE));
            entity.Property(e => e.Description)
                .HasMaxLength(DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH)
                .IsUnicode(DataDefinitions.IS_UNICODE_DEFAULT_VALUE);
            entity.Property(e => e.DueDate)
                .HasConversion(d => d.ToNullableDateOnly(), d => d.ToNullableDueDate());
            entity.Property(e => e.CompleteDate)
                .HasConversion(c => c.ToNullableDateTimeOffset(), c => c.ToNullableCompleteDate());
            entity.Property(e => e.Importance)
                .HasConversion(i => i.BoolValue, i => new Importance(i))
                .HasColumnName("IsImportant");
            entity.HasOne(e => e.Category)
                .WithMany(t => t.Todos)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Navigation(e => e.Category)
                .AutoInclude();
#pragma warning restore IDE0058
        });
}
