using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers(); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();