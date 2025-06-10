using MediatR;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Users.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Users.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync();
        }
    }
}