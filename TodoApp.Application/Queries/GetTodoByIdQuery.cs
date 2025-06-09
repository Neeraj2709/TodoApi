using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Queries
{
    public class GetTodoByIdQuery : IRequest<TodoResponse?>
    {
        public int Id { get; }

        public GetTodoByIdQuery(int id)
        {
            Id = id;
        }
    }
}