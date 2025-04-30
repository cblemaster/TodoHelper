
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess;
using TodoHelper.DataAccess.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));
builder.Services.AddScoped<ITodosRepository, TodosRepository>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome!");

app.Run();
