using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Queries
{
    public class GetAllTodosQuery : IRequest<List<TodoResponse>> { }
}
