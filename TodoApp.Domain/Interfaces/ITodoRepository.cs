using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Common.Interfaces;

public interface ITodoRepository
{
    Task<TodoResponse> CreateTodoAsync(TodoRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<List<TodoResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<TodoResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<TodoResponse>> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, TodoRequest request, CancellationToken cancellationToken);
}