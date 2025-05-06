
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess;

public sealed class TodosDbContext(DbContextOptions<TodosDbContext> options) : DbContext(options)
{
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#pragma warning disable IDE0058
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable(nameof(Category));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(i => i.Value, i => Identifier<Category>.Create(i));
            entity.Property(e => e.Name).HasConversion(n => n.Value, n => Descriptor.Create(n, nameof(Category.Name), DataConstants.CATEGORY_NAME_MAX_LENGTH).Value!);
            entity.Property(e => e.Name).HasMaxLength(DataConstants.CATEGORY_NAME_MAX_LENGTH).IsUnicode(DataConstants.IS_UNICODE);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.CreateDate).HasConversion(c => c.Value, c => CreateDate.Create(c));
            entity.Property(e => e.UpdateDate).HasConversion(u => u.Value, u => UpdateDate.Create(u));
            entity.Property(e => e.UpdateDate).IsRequired(DataConstants.IS_REQUIRED_VALUE_FOR_NULLABLE_PROPERTY);
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable(nameof(Todo));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(i => i.Value, i => Identifier<Todo>.Create(i));
            entity.Property(e => e.CategoryId).HasConversion(c => c.Value, c => Identifier<Category>.Create(c));
            entity.Property(e => e.Description).HasConversion(d => d.Value, d => Descriptor.Create(d, nameof(Todo.Description), 255).Value!);
            entity.Property(e => e.Description).HasMaxLength(DataConstants.CATEGORY_NAME_MAX_LENGTH).IsUnicode(DataConstants.IS_UNICODE);
            entity.Property(e => e.DueDate).HasConversion(d => d.Value, d => DueDate.Create(d));
            entity.Property(e => e.DueDate).IsRequired(DataConstants.IS_REQUIRED_VALUE_FOR_NULLABLE_PROPERTY);
            entity.Property(e => e.CompleteDate).HasConversion(c => c.Value, c => CompleteDate.Create(c));
            entity.Property(e => e.CompleteDate).IsRequired(DataConstants.IS_REQUIRED_VALUE_FOR_NULLABLE_PROPERTY);
            entity.Property(e => e.CreateDate).HasConversion(c => c.Value, c => CreateDate.Create(c));
            entity.Property(e => e.UpdateDate).HasConversion(u => u.Value, u => UpdateDate.Create(u));
            entity.Property(e => e.UpdateDate).IsRequired(DataConstants.IS_REQUIRED_VALUE_FOR_NULLABLE_PROPERTY);
            entity.Property(e => e.Importance).HasConversion(i => i.IsImportant, i => Importance.Create(i)).HasColumnName("IsImportant");
            entity.Ignore(e => e.IsComplete);
            entity.Ignore(e => e.CanBeUpdated);
            entity.Ignore(e => e.CanBeDeleted);
            entity.HasOne(e => e.Category)
                .WithMany(t => t.Todos)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

#pragma warning restore IDE0058
    }
}
