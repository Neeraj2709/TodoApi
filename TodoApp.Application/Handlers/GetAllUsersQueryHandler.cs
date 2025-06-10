using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Users.Queries;
using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Application.Users.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
{
    private readonly ApplicationDbContext _context;

    public GetAllUsersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return users;
    }
}