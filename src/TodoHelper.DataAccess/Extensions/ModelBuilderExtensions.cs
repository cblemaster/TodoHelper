
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Definitions;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Extensions;

internal static class ModelBuilderExtensions
{
    private const bool IsUnicodeDefaultValue = false;

    internal static ModelBuilder ConfigureCategoryEntity(this ModelBuilder builder) =>
        builder.Entity<Category>(entity =>
        {
#pragma warning disable IDE0058
            entity.ToTable(nameof(Category));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(i => i.Value, i => Identifier<Category>.Create(i));
            entity.Property(e => e.Name).HasConversion(n => n.Value, n => new Descriptor(n, DataDefinitions.CATEGORY_NAME_MAX_LENGTH, DataDefinitions.CATEGORY_NAME_ATTRIBUTE));
            entity.Property(e => e.Name).HasMaxLength(40).IsUnicode(IsUnicodeDefaultValue);
            entity.HasIndex(e => e.Name).IsUnique();
#pragma warning restore IDE0058
        });
    internal static ModelBuilder ConfigureTodoEntity(this ModelBuilder builder) =>
        builder.Entity<Todo>(entity =>
        {
#pragma warning disable IDE0058
            entity.ToTable(nameof(Todo));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(i => i.Value, i => Identifier<Todo>.Create(i));
            entity.Property(e => e.CategoryId).HasConversion(c => c.Value, c => Identifier<Category>.Create(c));
            entity.Property(e => e.Description).HasConversion(d => d.Value, d => new Descriptor(d, DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH, DataDefinitions.TODO_DESCRIPTION_ATTRIBUTE));
            entity.Property(e => e.Description).HasMaxLength(255).IsUnicode(IsUnicodeDefaultValue);
            entity.Property(e => e.DueDate).HasConversion(d => d!.Value.Value, d => new DueDate(d!.Value));
            entity.Property(e => e.CompleteDate).HasConversion(c => c!.Value.Value, c => new CompleteDate(c!.Value));
            entity.Property(e => e.Importance).HasConversion(i => i.IsImportant, i => new Importance(i)).HasColumnName("IsImportant");
            entity.HasOne(e => e.Category)
                .WithMany(t => t.Todos)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Navigation(e => e.Category).AutoInclude();
#pragma warning restore IDE0058
        });
}
