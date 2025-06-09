using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Validators;
using TodoApi.TodoApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<TodoRequestValidator>();
    });
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();