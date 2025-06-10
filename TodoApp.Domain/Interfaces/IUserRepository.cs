using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Domain.Entities;

namespace TodoApi.TodoApp.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse?> GetByIdAsync(int id);
    Task<UserResponse?> GetByUsernameAsync(string username);
    Task<UserResponse> CreateAsync(UserRequest request);
    Task<bool> UpdateUsernameAsync(int id, string newUsername);
    Task<bool> DeleteAsync(int id);
    Task<bool> UsernameExistsAsync(string username);
}