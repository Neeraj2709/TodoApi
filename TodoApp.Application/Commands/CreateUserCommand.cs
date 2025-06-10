using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public UserRequest UserRequest { get; }

        public CreateUserCommand(UserRequest userRequest)
        {
            UserRequest = userRequest;
        }
    }
}