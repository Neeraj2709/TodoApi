using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Users.Queries;

public class GetAllUsersQuery : IRequest<List<UserResponse>>
{
}