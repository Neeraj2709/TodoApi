using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Domain.Entities;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        return await _context.Users
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username
            })
            .ToListAsync();
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? null : new UserResponse { Id = user.Id, Username = user.Username };
    }

    public async Task<UserResponse?> GetByUsernameAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user == null ? null : new UserResponse { Id = user.Id, Username = user.Username };
    }

    public async Task<UserResponse> CreateAsync(UserRequest request)
    {
        var user = new User
        {
            Username = request.Username
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username
        };
    }

    public async Task<bool> UpdateUsernameAsync(int id, string newUsername)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Username = newUsername;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }
}
