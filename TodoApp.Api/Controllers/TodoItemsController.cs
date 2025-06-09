using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.TodoApp.Application.Commands;
using TodoApi.TodoApp.Application.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create Todo
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoRequest request)
        {
            var result = await _mediator.Send(new CreateTodoCommand(request));
            return CreatedAtAction(nameof(GetTodoById), new { id = result.Id }, result);
        }

        // Get All Todos
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var result = await _mediator.Send(new GetAllTodosQuery());
            return Ok(result);
        }

        // Get Todo by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _mediator.Send(new GetTodoByIdQuery(id));

            if (todo == null)
                return NotFound(new { message = "Todo not found" });

            return Ok(todo);
        }

        // Get Todo by Username
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetTodosByUsername(string username)
        {
            var todos = await _mediator.Send(new GetTodosByUsernameQuery(username));

            if (todos == null || !todos.Any())
                return NotFound(new { message = "No todos found for this user" });

            return Ok(todos);
        }

        // Update Todo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoRequest todoRequest)
        {
            var result = await _mediator.Send(new UpdateTodoCommand(id, todoRequest));

            if (result == null)
                return NotFound(new { message = "Todo not found or you are not authorized" });

            return Ok(result);
        }


        // Delete Todo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoById(int id)
        {
            var result = await _mediator.Send(new DeleteTodoCommand(id));

            if (!result)
                return NotFound(new { message = "Todo not found" });

            return Ok(new { message = "Todo deleted successfully" });
        }

    }
}
