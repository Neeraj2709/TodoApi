using MediatR;
using TodoApi.TodoApp.Application.Commands;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Handlers
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, TodoResponse?>
    {
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoResponse?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            return await _todoRepository.UpdateAsync(request.Id, request.TodoRequest, cancellationToken)
                ? new TodoResponse
                {
                    Id = request.Id,
                    Title = request.TodoRequest.Title,
                    IsCompleted = request.TodoRequest.IsCompleted,
                    Username = request.TodoRequest.Username
                }
                : null;
        }
    }
}