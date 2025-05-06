
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.Entities;

namespace TodoHelper.DataAccess;

public sealed class TodosDbContext(DbContextOptions<TodosDbContext> options) : DbContext(options)
{
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureCategoryEntity();
        modelBuilder.ConfigureTodoEntity();
    }
}
