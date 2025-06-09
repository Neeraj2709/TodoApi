using MediatR;

namespace TodoApi.TodoApp.Application.Commands
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteTodoCommand(int id)
        {
            Id = id;
        }
    }
}