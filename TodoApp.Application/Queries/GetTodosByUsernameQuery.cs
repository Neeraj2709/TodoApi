using TodoApi.TodoApp.Domain.DTOs;
using MediatR;

namespace TodoApi.TodoApp.Application.Queries
{
    public class GetTodosByUsernameQuery : IRequest<List<TodoResponse>>
    {
        public string Username { get; }

        public GetTodosByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
