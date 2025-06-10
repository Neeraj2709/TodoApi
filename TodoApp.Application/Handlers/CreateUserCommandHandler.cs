using MediatR;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Users.Commands;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.UsernameExistsAsync(request.UserRequest.Username);

            if (exists)
            {
                throw new Exception("Username already exists.");
            }

            var response = await _userRepository.CreateAsync(request.UserRequest);

            return response;
        }
    }
}