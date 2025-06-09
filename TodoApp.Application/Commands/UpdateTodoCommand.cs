using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Commands
{
    public class UpdateTodoCommand : IRequest<TodoResponse?>
    {
        public int Id { get; set; }
        public TodoRequest TodoRequest { get; set; }

        public UpdateTodoCommand(int id, TodoRequest todoRequest)
        {
            Id = id;
            TodoRequest = todoRequest;
        }
    }
}
