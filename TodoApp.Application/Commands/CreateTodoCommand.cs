using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Commands
{
    public class CreateTodoCommand : IRequest<TodoResponse>
    {
        public TodoRequest TodoRequest { get; }

        public CreateTodoCommand(TodoRequest todoRequest)
        {
            TodoRequest = todoRequest;
        }
    }
}