
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Extensions;

// TODO: magic numbers and bools
internal static class ModelBuilderExtensions
{
    internal static ModelBuilder ConfigureCategoryEntity(this ModelBuilder builder) =>
        builder.Entity<Category>(entity =>
        {
#pragma warning disable IDE0058
            entity.ToTable(nameof(Category));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(i => i.Value, i => Identifier<Category>.Create(i));
            entity.Property(e => e.Name).HasConversion(n => n.Value, n => new Descriptor(n, 40, "Category name"));
            entity.Property(e => e.Name).HasMaxLength(40).IsUnicode(false);
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
            entity.Property(e => e.Description).HasConversion(d => d.Value, d => new Descriptor(d, 255, "Todo description"));
            entity.Property(e => e.Description).HasMaxLength(255).IsUnicode(false);
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
