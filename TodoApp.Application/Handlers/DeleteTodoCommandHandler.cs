using MediatR;
using TodoApi.TodoApp.Application.Commands;
using TodoApi.TodoApp.Application.Common.Interfaces;

namespace TodoApi.TodoApp.Application.Handlers
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            return await _todoRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}