using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Application.Users.Commands;
using TodoApi.TodoApp.Domain.DTOs;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Application.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly ApplicationDbContext _context;

        public UpdateUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new Exception("User not found.");
            }
            //check if new username already exists
            var usernameExists = await _context.Users
                .AnyAsync(u => u.Username == request.NewUsername, cancellationToken);

            if (usernameExists)
            {
                throw new Exception("New username already exists.");
            }

            user.Username = request.NewUsername;
            await _context.SaveChangesAsync(cancellationToken);

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                CreatedAt = user.CreatedAt
            };
        }
    }
}