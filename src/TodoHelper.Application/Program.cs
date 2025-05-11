
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));
WebApplication app = builder.Build();

app.Run();
