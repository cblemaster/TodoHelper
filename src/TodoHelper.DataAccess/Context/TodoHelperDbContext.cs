
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Extensions;

namespace TodoHelper.DataAccess.Context;

public sealed class TodoHelperDbContext(DbContextOptions<TodoHelperDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#pragma warning disable IDE0058
        modelBuilder.ConfigureCategoryEntity();
        modelBuilder.ConfigureTodoEntity();
#pragma warning restore IDE0058
    }
}
