using MediatR;
using TodoApi.TodoApp.Application.Common.Interfaces;
using TodoApi.TodoApp.Application.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Handlers
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoResponse>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllTodosQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<TodoResponse>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            return await _todoRepository.GetAllAsync(cancellationToken);
        }
    }
}