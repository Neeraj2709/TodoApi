using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Domain.Entities;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Application.Commands

{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, TodoResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateTodoHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.TodoRequest.Username);

            if (user == null)
                throw new Exception("User not found");

            var todo = new TodoItem
            {
                Title = request.TodoRequest.Title,
                IsCompleted = request.TodoRequest.IsCompleted,
                UserId = user.Id
            };

            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();

            return new TodoResponse
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                Username = user.Username
            };
        }
    }
}