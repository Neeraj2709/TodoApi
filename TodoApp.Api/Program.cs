using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Validators;
using TodoApi.TodoApp.Infrastructure.Data;
using TodoApi.TodoApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // Register all validators in the Application project
        fv.RegisterValidatorsFromAssemblyContaining<TodoRequestValidator>();
    });

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR from the Application layer
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ITodoRepository).Assembly));

// Register Repositories (infrastructure)
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();