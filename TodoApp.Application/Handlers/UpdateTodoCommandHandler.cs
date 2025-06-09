using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Commands;
using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Application.Handlers
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, TodoResponse?>
    {
        private readonly ApplicationDbContext _context;

        public UpdateTodoCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoResponse?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _context.TodoItems.FindAsync(request.Id);

            if (todo == null) return null;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.TodoRequest.Username, cancellationToken);

            if (user == null || user.Id != todo.UserId) return null;

            if (string.IsNullOrWhiteSpace(request.TodoRequest.Title)) return null;

            todo.Title = request.TodoRequest.Title;
            todo.IsCompleted = request.TodoRequest.IsCompleted;

            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync(cancellationToken);

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