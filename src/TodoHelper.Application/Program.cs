
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.Extensions;
using TodoHelper.DataAccess.Context;
using TodoHelper.DataAccess.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.RegisterHandlers();
WebApplication app = builder.Build();

app.MapEndpoints();
app.Run();
