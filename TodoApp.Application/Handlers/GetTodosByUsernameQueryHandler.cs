using MediatR;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Handlers
{
    public class GetTodosByUsernameQueryHandler : IRequestHandler<GetTodosByUsernameQuery, List<TodoResponse>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodosByUsernameQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<TodoResponse>> Handle(GetTodosByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await _todoRepository.GetByUsernameAsync(request.Username, cancellationToken);
        }
    }
}