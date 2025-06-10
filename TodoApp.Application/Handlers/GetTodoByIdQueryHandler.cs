using MediatR;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Handlers
{
    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoResponse?>
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodoByIdQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoResponse?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _todoRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}