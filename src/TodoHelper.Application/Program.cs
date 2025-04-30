
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess;
using TodoHelper.DataAccess.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer("Server=.;Database=TodoHelper;Trusted_Connection=true;Trust Server Certificate=true"));
builder.Services.AddScoped<ITodosRepository, TodosRepository>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome!");

app.Run();
