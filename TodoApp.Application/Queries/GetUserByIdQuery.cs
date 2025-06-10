using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Users.Queries;

public class GetUserByIdQuery : IRequest<UserResponse?>
{
    public int Id { get; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}