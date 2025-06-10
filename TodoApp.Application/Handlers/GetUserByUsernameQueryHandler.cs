using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Users.Queries;
using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Application.Users.Handlers;

public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserResponse?>
{
    private readonly ApplicationDbContext _context;

    public GetUserByUsernameQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(u => u.Username == request.Username)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                CreatedAt = u.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }
}