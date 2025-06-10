using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Domain.Entities;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoResponse> CreateTodoAsync(TodoRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            if (user == null)
                throw new Exception("User not found");

            var todo = new TodoItem
            {
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                UserId = user.Id
            };

            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync(cancellationToken);

            return new TodoResponse
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                Username = user.Username
            };
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var todo = await _context.TodoItems.FindAsync(new object[] { id }, cancellationToken);
            if (todo == null) return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<TodoResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.TodoItems
                .Include(t => t.User)
                .Select(t => new TodoResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted,
                    Username = t.User.Username
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<TodoResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var todo = await _context.TodoItems
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        
            if (todo == null) return null;
        
            return new TodoResponse
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                Username = todo.User.Username
            };
        }

        public async Task<List<TodoResponse>> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

            if (user == null) return new List<TodoResponse>();

            return await _context.TodoItems
                .Where(t => t.UserId == user.Id)
                .Select(t => new TodoResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted,
                    Username = user.Username
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(int id, TodoRequest request, CancellationToken cancellationToken)
        {
            var todo = await _context.TodoItems.FindAsync(new object[] { id }, cancellationToken);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            if (todo == null || user == null || user.Id != todo.UserId)
                return false;

            todo.Title = request.Title;
            todo.IsCompleted = request.IsCompleted;

            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        // public async Task<bool> UpdateStatusAsync(int id, string username, bool isCompleted, CancellationToken cancellationToken)
        // {
        //     var todo = await _context.TodoItems.FindAsync(new object[] { id }, cancellationToken);
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
        //
        //     if (todo == null || user == null || user.Id != todo.UserId)
        //         return false;
        //
        //     todo.IsCompleted = isCompleted;
        //
        //     _context.TodoItems.Update(todo);
        //     await _context.SaveChangesAsync(cancellationToken);
        //
        //     return true;
        // }
    }
}

