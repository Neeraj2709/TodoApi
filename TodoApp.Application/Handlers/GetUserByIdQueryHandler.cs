using MediatR;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Users.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(request.Id);
        }
    }
}