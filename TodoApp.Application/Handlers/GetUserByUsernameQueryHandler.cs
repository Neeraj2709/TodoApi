using MediatR;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Users.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Users.Handlers
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByUsernameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByUsernameAsync(request.Username);
        }
    }
}