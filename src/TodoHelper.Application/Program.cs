
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.Features.CreateCategory;
using TodoHelper.Application.Features.CreateTodo;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess;
using TodoHelper.DataAccess.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.Services.AddScoped<ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>, CreateCategoryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateTodoCommand, CreateTodoResponse>, CreateTodoHandler>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome!");

app.Run();
