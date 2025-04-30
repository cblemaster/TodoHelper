
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess;

public sealed class TodosDbContext : DbContext
{
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Todo> Todos { get; set; }

    public TodosDbContext(DbContextOptions<TodosDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#pragma warning disable IDE0058
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable(nameof(Category));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(i => i.Value, i => Identifier<Category>.Create(i));
            entity.Property(e => e.Name).HasConversion(n => n.Value, n => Name.Create(n).Value!);
            entity.Property(e => e.Name).HasMaxLength(Name.MAX_LENGTH).IsUnicode(false);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Ignore(e => e.CanBeDeleted);
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable(nameof(Todo));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(i => i.Value, i => Identifier<Todo>.Create(i));
            entity.Property(e => e.CategoryId).HasConversion(c => c.Value, c => Identifier<Category>.Create(c));
            entity.Property(e => e.Description).HasConversion(d => d.Value, d => Description.Create(d).Value!);
            entity.Property(e => e.Description).HasMaxLength(Description.MAX_LENGTH).IsUnicode(false);
            entity.Property(e => e.DueDate).HasConversion(d => d.Value, d => DueDate.Create(d));
            entity.Property(e => e.DueDate).IsRequired(false);
            entity.Property(e => e.CompleteDate).HasConversion(c => c.Value, c => CompleteDate.Create(c));
            entity.Property(e => e.CompleteDate).IsRequired(false);
            entity.Property(e => e.CreateDate).HasConversion(c => c.Value, c => CreateDate.Create(c));
            entity.Property(e => e.UpdateDate).HasConversion(u => u.Value, u => UpdateDate.Create(u));
            entity.Property(e => e.UpdateDate).IsRequired(false);
            entity.Property(e => e.Importance).HasConversion(i => i.IsImportant, i => Importance.Create(i)).HasColumnName("IsImportant");
            entity.Ignore(e => e.IsComplete);
            entity.Ignore(e => e.CanBeUpdated);
            entity.Ignore(e => e.CanBeDeleted);
            entity.HasOne(e => e.Category)
                .WithMany(t => t.Todos)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Category>().Navigation(e => e.Todos).AutoInclude();
        modelBuilder.Entity<Todo>().Navigation(e => e.Category).AutoInclude();
#pragma warning restore IDE0058
    }
}
