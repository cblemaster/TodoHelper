
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Extensions;
using TodoHelper.Domain.Entities;

namespace TodoHelper.DataAccess.Context;

public sealed class TodosDbContext(DbContextOptions<TodosDbContext> options) : DbContext(options)
{
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#pragma warning disable IDE0058
        modelBuilder.ConfigureCategoryEntity();
        modelBuilder.ConfigureTodoEntity();
#pragma warning restore IDE0058
    }
}
